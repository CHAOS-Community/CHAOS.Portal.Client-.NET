using System;

namespace CHAOS.Portal.Client.Managers
{
	public interface IUserProfileManager
	{
		void Get<T>(Action<bool, T> callback, Guid metadataSchemaGuid, Guid? userGuid = null) where T: class;
	}
}