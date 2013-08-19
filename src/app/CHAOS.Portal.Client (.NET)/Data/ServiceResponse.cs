using System;

namespace CHAOS.Portal.Client.Data
{
	public class ServiceResponse<T> where T : class, IServiceBody
	{
		public ServiceHeader Header { get; set; }
		public T Body { get; set; }
		public Exception Error { get; set; }
	}
}