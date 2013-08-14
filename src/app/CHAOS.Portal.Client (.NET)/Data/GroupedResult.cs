using System.Collections.Generic;

namespace CHAOS.Portal.Client.Data
{
	public class GroupedResult<T> : IServiceResult
	{
		 public IList<ResultGroup<T>> Groups { get; set; }
	}
}