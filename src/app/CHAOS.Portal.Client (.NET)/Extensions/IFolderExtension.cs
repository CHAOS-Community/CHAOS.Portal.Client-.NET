using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IFolderExtension
	{
		IServiceCallState<IServiceResult_MCM<Folder>> Get(uint? id, int? folderTypeID, uint? parentID);
		IServiceCallState<IServiceResult_MCM<Folder>> Create(string subscriptionGUID, string title, uint? parentID, int folderTypeID);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Update(uint id, string newTitle, int? newParentID, int? newFolderTypeID);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Delete(uint id);
		IServiceCallState<IServiceResult_MCM<Permissions<FolderPermissions>>> GetPermission(uint folderID);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> SetPermission(Guid? userGUID, Guid? groupGUID, uint folderID, FolderPermissions permission);
	}
}