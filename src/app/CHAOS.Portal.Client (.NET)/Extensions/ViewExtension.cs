using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public class ViewExtension : AExtension, IViewExtension
	{
		public IServiceCallState<T> Get<T>(string view, string query, string facet, string sort, int pageIndex, int pageSize) where T : class
		{
			return CallService<T>(HTTPMethod.GET, view, query, facet, sort, pageIndex, pageSize);
		}

		public IServiceCallState<ViewInfo> List()
		{
			return CallService<ViewInfo>(HTTPMethod.GET);
		}
	}
}