using System;
using CHAOS.Events;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.Managers;
using CHAOS.Extensions;

namespace CHAOS.Portal.Client.Standard.Managers
{
	public class UserManager : IUserManager
	{
		public event EventHandler<DataEventArgs<Exception>> FailedToGetCurrentUser = delegate { };

		private readonly IPortalClient _client;
		private User _currentUser;

		public UserManager(IPortalClient client)
		{
			_client = client.ValidateIsNotNull("client");
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
			oldUser.ID = newUser.ID;
			oldUser.SessionID = newUser.SessionID;
			oldUser.GUID = newUser.GUID;
			oldUser.Firstname = newUser.Firstname;
			oldUser.Middlename = newUser.Middlename;
			oldUser.Lastname = newUser.Lastname;
			oldUser.Email = newUser.Email;
			oldUser.SystemPermission = newUser.SystemPermission;
		}
	}
}