using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IFolderTypeExtension
	{
		IServiceCallState<IServiceResult_MCM<FolderType>> Get(int? id = null, string name = null);
		IServiceCallState<IServiceResult_MCM<FolderType>> Create(string name);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Update(int id, string name);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Delete(int id);
	}
}