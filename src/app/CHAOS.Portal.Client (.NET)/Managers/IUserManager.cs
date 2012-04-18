using System;
using CHAOS.Common.Events;
using CHAOS.Portal.Client.Data.Portal;

namespace CHAOS.Portal.Client.Managers
{
	public interface IUserManager
	{
		event EventHandler<DataEventArgs<Exception>> FailedToGetCurrentUser;
		
		User GetCurrentUser();
	}
}