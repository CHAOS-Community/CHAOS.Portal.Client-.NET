using System.Collections.Generic;

namespace CHAOS.Portal.Client.Data
{
	public class ServiceResponse<T>
	{
		public ServiceHeader Header { get; set; }
		public ServiceResult<T> Result { get; set; }
	}

	public class ServiceResult<T>
	{
		public uint Count { get; set; }
		public uint TotalCount { get; set; }
		public IList<T> Results { get; set; }
	}
}