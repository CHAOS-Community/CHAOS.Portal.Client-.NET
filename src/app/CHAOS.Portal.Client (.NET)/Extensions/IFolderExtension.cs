using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IFolderExtension
	{
		IServiceCallState<IServiceResult_MCM<Folder>> Get(int? id, int? folderTypeID, int? parentID);
		IServiceCallState<IServiceResult_MCM<Folder>> Create(string subscriptionGUID, string title, int? parentID, int folderTypeID);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Update(int id, string newTitle, int? newParentID, int? newFolderTypeID);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Delete(int id);
	}
}