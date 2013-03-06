using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IViewExtension
	{
		IServiceCallState<T> Get<T>(string view, string query, string facet, string sort, uint pageIndex, uint pageSize) where T : class;
		IServiceCallState<ViewInfo> List();
	}
}