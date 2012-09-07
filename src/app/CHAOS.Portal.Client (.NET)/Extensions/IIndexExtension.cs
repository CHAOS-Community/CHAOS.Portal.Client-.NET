using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Indexing;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IIndexExtension
	{
		IServiceCallState<IServiceResult_Index<IndexResponse>> Search(string query, string facet, string sort, uint pageIndex, uint pageSize, Guid? accessPointGUID = null);
	}
}