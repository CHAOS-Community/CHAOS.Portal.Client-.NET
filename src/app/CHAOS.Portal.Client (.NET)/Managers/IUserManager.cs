using System;
using System.Xml.Linq;
using CHAOS.Events;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;

namespace CHAOS.Portal.Client.Managers
{
	public interface IUserManager
	{
		event EventHandler<DataEventArgs<Exception>> FailedToGetCurrentUser;
		
		User GetCurrentUser();

		void GetUserProfile(User user, MetadataSchema schema, Action<XElement> callback);
		void GetUserProfile<T>(User user, MetadataSchema schema, Action<XElement, T> callback, T token);

		void GetUserProfile(Guid userGUID, Guid schemaGUID, Action<XElement> callback);
		void GetUserProfile<T>(Guid userGUID, Guid schemaGUID, Action<XElement, T> callback, T token);
	}
}