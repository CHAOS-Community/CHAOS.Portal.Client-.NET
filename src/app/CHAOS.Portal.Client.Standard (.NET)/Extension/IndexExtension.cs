using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Indexing;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class IndexExtension : AExtension, IIndexExtension
	{
		public IndexExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }

		public IServiceCallState<IServiceResult_Portal<IndexResponse>> Search(string facet)
		{
			return CallService<IServiceResult_Portal<IndexResponse>>(HTTPMethod.GET, facet);
		}
	}
}