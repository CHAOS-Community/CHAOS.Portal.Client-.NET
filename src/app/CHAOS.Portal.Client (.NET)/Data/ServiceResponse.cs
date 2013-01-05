using System;

namespace CHAOS.Portal.Client.Data
{
	public class ServiceResponse<T>
	{
		public ServiceHeader Header { get; set; }
		public ServiceResult<T> Result { get; set; }
		public Exception Error { get; set; }
	}
}