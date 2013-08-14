using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IFolderTypeExtension
	{
		IServiceCallState<PagedResult<FolderType>> Get(int? id = null, string name = null);
		IServiceCallState<PagedResult<FolderType>> Create(string name);
		IServiceCallState<PagedResult<ScalarResult>> Update(int id, string name);
		IServiceCallState<PagedResult<ScalarResult>> Delete(int id);
	}
}