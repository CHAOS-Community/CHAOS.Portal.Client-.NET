using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Xml.Linq;
using CHAOS.Events;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.Managers;
using CHAOS.Extensions;
using System.Linq;

namespace CHAOS.Portal.Client.Standard.Managers
{
	public class UserManager : IUserManager
	{
		private readonly IObjectManager _objectManager;
		public event EventHandler<DataEventArgs<Exception>> FailedToGetCurrentUser = delegate { };

		private readonly IPortalClient _client;
		private User _currentUser;

		public UserManager(IPortalClient client, IObjectManager objectManager)
		{
			_client = client.ValidateIsNotNull("client");
			_objectManager = objectManager.ValidateIsNotNull("objectManager");
		}

		public User GetCurrentUser()
		{
			if(_currentUser == null)
				_currentUser = new User();

			var state = _client.User.Get();
			state.Callback += GetCurrentUserCompleted;
			state.FeedbackOnDispatcher = true;

			return _currentUser;
		}

		private void GetCurrentUserCompleted(IServiceResult_Portal<User> result, Exception error, object token)
		{
			if(error != null)
			{
				FailedToGetCurrentUser(this, new DataEventArgs<Exception>(error));
				return;
			}
			if(result.Portal.Error != null)
			{
				FailedToGetCurrentUser(this, new DataEventArgs<Exception>(result.Portal.Error));
				return;
			}
			if(result.Portal.Data.Count == 0)
			{
				FailedToGetCurrentUser(this, new DataEventArgs<Exception>(new Exception("No user returned")));
				return;
			}

			UpdateUser(_currentUser, result.Portal.Data[0]);
		}

		private static void UpdateUser(User oldUser, User newUser)
		{
			oldUser.SessionID = newUser.SessionID;
			oldUser.GUID = newUser.GUID;
			oldUser.Firstname = newUser.Firstname;
			oldUser.Middlename = newUser.Middlename;
			oldUser.Lastname = newUser.Lastname;
			oldUser.Email = newUser.Email;
			oldUser.SystemPermission = newUser.SystemPermission;
		}

		#region UserProfile

		public void GetUserProfile(User user, MetadataSchema schema, Action<XElement> callback)
		{
			GetUserProfile(user.ValidateIsNotNull("user").GUID.Value, schema.ValidateIsNotNull("schema").GUID, callback);
		}

		public void GetUserProfile<T>(User user, MetadataSchema schema, Action<XElement, T> callback, T token)
		{
			GetUserProfile(user, schema, xe => callback(xe, token));
		}

		public void GetUserProfile(Guid userGUID, Guid schemaGUID, Action<XElement> callback)
		{
			var userObject = _objectManager.GetObjectByGUID(userGUID, false, true);
			var metadata = userObject.Metadatas.DoIfIsNotNull(ms => ms.FirstOrDefault(m => m.MetadataSchemaGUID == schemaGUID));

			if (metadata != null)
				callback(metadata.MetadataXML);
			else
			{
				NotifyCollectionChangedEventHandler metadatasChangedAction = null;
				metadatasChangedAction = (sender, args) =>
					                         {
						                         if (args.Action != NotifyCollectionChangedAction.Add) return;

						                         args.NewItems.Cast<Metadata>()
						                             .FirstOrDefault(m => m.MetadataSchemaGUID == schemaGUID)
						                             .DoIfIsNotNull(m =>
							                                            {
																			userObject.Metadatas.CollectionChanged -= metadatasChangedAction;
								                                            callback(m.MetadataXML);
							                                            });
					                         };

				if (userObject.Metadatas == null)
				{
					PropertyChangedEventHandler objectChangedAction = null;
					objectChangedAction = (sender, args) =>
						                      {
							                      if (args.PropertyName != "Metadatas") return;

							                      userObject.PropertyChanged -= objectChangedAction;

							                      var profileMetadata = userObject.Metadatas.FirstOrDefault(m => m.MetadataSchemaGUID == schemaGUID);

												  if (profileMetadata != null)
													  callback(profileMetadata.MetadataXML);
												  else
													userObject.Metadatas.CollectionChanged += metadatasChangedAction;
						                      };

					userObject.PropertyChanged += objectChangedAction;
				}
				else
					userObject.Metadatas.CollectionChanged += metadatasChangedAction;
			}
		}

		public void GetUserProfile<T>(Guid userGUID, Guid schemaGUID, Action<XElement, T> callback, T token)
		{
			GetUserProfile(userGUID, schemaGUID, xe => callback(xe, token));
		}

		#endregion
	}
}