using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IFolderExtension
	{
		IServiceCallState<PagedResult<Folder>> Get(uint? id = null, uint? folderTypeID = null, uint? parentID = null);
		IServiceCallState<PagedResult<Folder>> Create(string subscriptionGUID, string title, uint? parentID, int folderTypeID);
		IServiceCallState<PagedResult<ScalarResult>> Update(uint id, string newTitle, uint? newParentID, uint? newFolderTypeID);
		IServiceCallState<PagedResult<ScalarResult>> Delete(uint id);
		IServiceCallState<PagedResult<Permissions<FolderPermissions>>> GetPermission(uint folderID);
		IServiceCallState<PagedResult<ScalarResult>> SetPermission(Guid? userGUID, Guid? groupGUID, uint folderID, FolderPermissions permission);
	}
}