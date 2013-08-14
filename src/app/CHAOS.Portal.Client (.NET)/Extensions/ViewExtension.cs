using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public class ViewExtension : AExtension, IViewExtension
	{
		public IServiceCallState<PagedResult<T>> Get<T>(string view, string query, string facet, string sort, uint pageIndex, uint pageSize) where T : class
		{
			return CallService<PagedResult<T>>(HTTPMethod.GET, view, query, facet, sort, pageIndex, pageSize);
		}

		public IServiceCallState<PagedResult<ViewInfo>> List()
		{
			return CallService<PagedResult<ViewInfo>>(HTTPMethod.GET);
		}
	}
}