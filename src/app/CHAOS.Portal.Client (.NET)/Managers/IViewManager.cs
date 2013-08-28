using CHAOS.Portal.Client.Managers.Data;

namespace CHAOS.Portal.Client.Managers
{
	public interface IViewManager
	{
		IManagerResult<T> Get<T>(string view, string query, string facet, string sort, string filter, uint pageSize) where T : class;
	}
}