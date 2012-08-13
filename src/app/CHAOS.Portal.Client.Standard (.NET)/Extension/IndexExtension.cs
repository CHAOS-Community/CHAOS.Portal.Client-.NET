using System;
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

		public IServiceCallState<IServiceResult_Portal<IndexResponse>> Search(string query, string facet, string sort, uint pageIndex, uint pageSize, Guid? accessPointGUID = null)
		{
			return CallService<IServiceResult_Portal<IndexResponse>>(HTTPMethod.GET, query, facet, sort, pageIndex, pageSize, accessPointGUID);
		}
	}
}