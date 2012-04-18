using System.Collections.Generic;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.ServiceCall
{
	public interface IServiceCall<T> where T : class, IServiceResult
	{
		IServiceCallState<T> State { get; }

		void Call(string servicePath, IDictionary<string, object> parameters, HTTPMethod method);
	}
}