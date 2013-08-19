using System.Collections.Generic;

namespace CHAOS.Portal.Client.Data
{
	public class GroupedResult<T> : IServiceBody
	{
		 public IList<ResultGroup<T>> Groups { get; set; }
	}
}