using System.Collections.Generic;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;
using HTTPMethod = CHAOS.Web.HTTPMethod;

namespace CHAOS.Portal.Client.Standard.ServiceCall
{
	public interface IServiceCall<T> where T : class, IServiceBody
	{
		IServiceCallState<T> State { get; }

		void Call(string servicePath, IDictionary<string, object> parameters, HTTPMethod method);
	}
}