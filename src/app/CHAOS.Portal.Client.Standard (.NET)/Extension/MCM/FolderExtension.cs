using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.Extensions.MCM;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension.MCM
{
	public class FolderExtension : Extension, IFolderExtension
	{
		public FolderExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }

		public IServiceCallState<IServiceResult_MCM<Folder>> Get(int? id, int? folderTypeID, int? parentID)
		{
			return CallService<IServiceResult_MCM<Folder>>(HTTPMethod.GET, id, folderTypeID, parentID);
		}

		public IServiceCallState<IServiceResult_MCM<Folder>> Create(string subscriptionGUID, string title, int? parentID, int folderTypeID)
		{
			return CallService<IServiceResult_MCM<Folder>>(HTTPMethod.GET, subscriptionGUID, title, parentID, folderTypeID);
		}

		public IServiceCallState<IServiceResult_MCM<ScalarResult>> Update(int id, string newTitle, int? newParentID, int? newFolderTypeID)
		{
			return CallService<IServiceResult_MCM<ScalarResult>>(HTTPMethod.GET, id, newTitle, newParentID, newFolderTypeID);
		}

		public IServiceCallState<IServiceResult_MCM<ScalarResult>> Delete(int id)
		{
			return CallService<IServiceResult_MCM<ScalarResult>>(HTTPMethod.GET, id);
		}
	}
}