using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IFolderTypeExtension
	{
		IServiceCallState<FolderType> Get(int? id = null, string name = null);
		IServiceCallState<FolderType> Create(string name);
		IServiceCallState<ScalarResult> Update(int id, string name);
		IServiceCallState<ScalarResult> Delete(int id);
	}
}