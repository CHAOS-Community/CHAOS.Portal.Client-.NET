using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Xml.Linq;
using CHAOS.Events;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.MCM.Data;
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

			var state = _client.User().Get();
			state.Callback += GetCurrentUserCompleted;
			state.FeedbackOnDispatcher = true;

			return _currentUser;
		}

		private void GetCurrentUserCompleted(ServiceResponse<PagedResult<User>> response, object token)
		{
			if (response.Error != null)
			{
				FailedToGetCurrentUser(this, new DataEventArgs<Exception>(response.Error));
				return;
			}
			if(response.Result.Results.Count == 0)
			{
				FailedToGetCurrentUser(this, new DataEventArgs<Exception>(new Exception("No user returned")));
				return;
			}

			UpdateUser(_currentUser, response.Result.Results[0]);
		}

		private static void UpdateUser(User oldUser, User newUser)
		{
			oldUser.Guid = newUser.Guid;
			oldUser.Email = newUser.Email;
			oldUser.SystemPermission = newUser.SystemPermission;
		}

		#region UserProfile

		public void GetUserProfile(User user, MetadataSchema schema, Action<XElement> callback)
		{
			GetUserProfile(user.ValidateIsNotNull("user").Guid.Value, schema.ValidateIsNotNull("schema").Guid, callback);
		}

		public void GetUserProfile<T>(User user, MetadataSchema schema, Action<XElement, T> callback, T token)
		{
			GetUserProfile(user, schema, xe => callback(xe, token));
		}

		public void GetUserProfile(Guid userGUID, Guid schemaGUID, Action<XElement> callback)
		{
			var userObject = _objectManager.GetObjectByGUID(userGUID, false, true);
			var metadata = userObject.Metadatas.DoIfIsNotNull(ms => ms.FirstOrDefault(m => m.MetadataSchemaGuid == schemaGUID));

			if (metadata != null)
				callback(metadata.MetadataXml);
			else
			{
				NotifyCollectionChangedEventHandler metadatasChangedAction = null;
				metadatasChangedAction = (sender, args) =>
					                         {
						                         if (args.Action != NotifyCollectionChangedAction.Add) return;

						                         args.NewItems.Cast<Metadata>()
						                             .FirstOrDefault(m => m.MetadataSchemaGuid == schemaGUID)
						                             .DoIfIsNotNull(m =>
							                                            {
																			userObject.Metadatas.CollectionChanged -= metadatasChangedAction;
								                                            callback(m.MetadataXml);
							                                            });
					                         };

				if (userObject.Metadatas == null)
				{
					PropertyChangedEventHandler objectChangedAction = null;
					objectChangedAction = (sender, args) =>
						                      {
							                      if (args.PropertyName != "Metadatas") return;

							                      userObject.PropertyChanged -= objectChangedAction;

							                      var profileMetadata = userObject.Metadatas.FirstOrDefault(m => m.MetadataSchemaGuid == schemaGUID);

												  if (profileMetadata != null)
													  callback(profileMetadata.MetadataXml);
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