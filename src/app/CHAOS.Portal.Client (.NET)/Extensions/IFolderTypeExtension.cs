using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IFolderTypeExtension
	{
		IServiceCallState<IServiceResult_MCM<FolderType>> Get(int? id, string name);
		IServiceCallState<IServiceResult_MCM<FolderType>> Create(string name);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Update(int id, string name);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Delete(int id);
	}
}