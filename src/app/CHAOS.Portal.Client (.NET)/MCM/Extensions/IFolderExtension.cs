using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IFolderExtension
	{
		IServiceCallState<Folder> Get(uint? id = null, uint? folderTypeID = null, uint? parentID = null);
		IServiceCallState<Folder> Create(string subscriptionGUID, string title, uint? parentID, int folderTypeID);
		IServiceCallState<ScalarResult> Update(uint id, string newTitle, uint? newParentID, uint? newFolderTypeID);
		IServiceCallState<ScalarResult> Delete(uint id);
		IServiceCallState<Permissions<FolderPermissions>> GetPermission(uint folderID);
		IServiceCallState<ScalarResult> SetPermission(Guid? userGUID, Guid? groupGUID, uint folderID, FolderPermissions permission);
	}
}