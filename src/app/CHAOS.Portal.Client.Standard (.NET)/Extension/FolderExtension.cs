using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class FolderExtension : AExtension, IFolderExtension
	{
		public FolderExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }

		public IServiceCallState<IServiceResult_MCM<Folder>> Get(uint? id, int? folderTypeID, uint? parentID)
		{
			return CallService<IServiceResult_MCM<Folder>>(HTTPMethod.GET, id, folderTypeID, parentID);
		}

		public IServiceCallState<IServiceResult_MCM<Folder>> Create(string subscriptionGUID, string title, uint? parentID, int folderTypeID)
		{
			return CallService<IServiceResult_MCM<Folder>>(HTTPMethod.GET, subscriptionGUID, title, parentID, folderTypeID);
		}

		public IServiceCallState<IServiceResult_MCM<ScalarResult>> Update(uint id, string newTitle, int? newParentID, int? newFolderTypeID)
		{
			return CallService<IServiceResult_MCM<ScalarResult>>(HTTPMethod.GET, id, newTitle, newParentID, newFolderTypeID);
		}

		public IServiceCallState<IServiceResult_MCM<ScalarResult>> Delete(uint id)
		{
			return CallService<IServiceResult_MCM<ScalarResult>>(HTTPMethod.GET, id);
		}

		public IServiceCallState<IServiceResult_MCM<Permissions<FolderPermissions>>> GetPermission(uint folderID)
		{
			return CallService<IServiceResult_MCM<Permissions<FolderPermissions>>>(HTTPMethod.GET, folderID);
		}

		public IServiceCallState<IServiceResult_MCM<ScalarResult>> SetPermission(Guid? userGUID, Guid? groupGUID, uint folderID, FolderPermissions permission)
		{
			return CallService<IServiceResult_MCM<ScalarResult>>(HTTPMethod.GET, userGUID, groupGUID, folderID, permission);
		}
	}
}