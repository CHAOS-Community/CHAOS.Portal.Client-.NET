using System.Collections.Generic;

namespace CHAOS.Portal.Client.Data
{
	public class ServiceResult<T>
	{
		public uint Count { get; set; }
		public uint TotalCount { get; set; }
		public IList<T> Results { get; set; }
	}
}