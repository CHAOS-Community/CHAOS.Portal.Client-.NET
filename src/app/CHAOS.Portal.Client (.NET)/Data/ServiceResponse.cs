using System;

namespace CHAOS.Portal.Client.Data
{
	public class ServiceResponse<T> where T : class, IServiceResult
	{
		public ServiceHeader Header { get; set; }
		public T Result { get; set; }
		public Exception Error { get; set; }
	}
}