using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public class FolderExtension : AExtension, IFolderExtension
	{
		public IServiceCallState<Folder> Get(uint? id, uint? folderTypeID, uint? parentID)
		{
			return CallService<Folder>(HTTPMethod.GET, id, folderTypeID, parentID);
		}

		public IServiceCallState<Folder> Create(string subscriptionGUID, string title, uint? parentID, int folderTypeID)
		{
			return CallService<Folder>(HTTPMethod.GET, subscriptionGUID, title, parentID, folderTypeID);
		}

		public IServiceCallState<ScalarResult> Update(uint id, string newTitle, uint? newParentID, uint? newFolderTypeID)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, id, newTitle, newParentID, newFolderTypeID);
		}

		public IServiceCallState<ScalarResult> Delete(uint id)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, id);
		}

		public IServiceCallState<Permissions<FolderPermissions>> GetPermission(uint folderID)
		{
			return CallService<Permissions<FolderPermissions>>(HTTPMethod.GET, folderID);
		}

		public IServiceCallState<ScalarResult> SetPermission(Guid? userGUID, Guid? groupGUID, uint folderID, FolderPermissions permission)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, userGUID, groupGUID, folderID, (uint)permission);
		}
	}
}