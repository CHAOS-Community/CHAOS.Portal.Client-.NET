using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Indexing;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IIndexExtension
	{
		IServiceCallState<IServiceResult_Portal<IndexResponse>> Search(string facet);
	}
}