using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions.MCM
{
	public interface IObjectExtension
	{
		IServiceCallState<IServiceResult_MCM<Object>> Get(string query, string sort, bool includeMetadata, bool includeFiles, bool includeObjectRelations, int pageIndex, int pageSize);
	}
}