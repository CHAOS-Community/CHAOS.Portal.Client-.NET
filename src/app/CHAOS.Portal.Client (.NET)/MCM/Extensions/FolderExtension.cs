using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public class FolderExtension : AExtension, IFolderExtension
	{
		public IServiceCallState<PagedResult<Folder>> Get(uint? id, uint? folderTypeID, uint? parentID)
		{
			return CallService<PagedResult<Folder>>(HTTPMethod.GET, id, folderTypeID, parentID);
		}

		public IServiceCallState<PagedResult<Folder>> Create(string subscriptionGUID, string title, uint? parentID, int folderTypeID)
		{
			return CallService<PagedResult<Folder>>(HTTPMethod.GET, subscriptionGUID, title, parentID, folderTypeID);
		}

		public IServiceCallState<PagedResult<ScalarResult>> Update(uint id, string newTitle, uint? newParentID, uint? newFolderTypeID)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.GET, id, newTitle, newParentID, newFolderTypeID);
		}

		public IServiceCallState<PagedResult<ScalarResult>> Delete(uint id)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.GET, id);
		}

		public IServiceCallState<PagedResult<Permissions<FolderPermissions>>> GetPermission(uint folderID)
		{
			return CallService<PagedResult<Permissions<FolderPermissions>>>(HTTPMethod.GET, folderID);
		}

		public IServiceCallState<PagedResult<ScalarResult>> SetPermission(Guid? userGUID, Guid? groupGUID, uint folderID, FolderPermissions permission)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.GET, userGUID, groupGUID, folderID, (uint)permission);
		}
	}
}