using System.Collections.Generic;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.ServiceCall
{
	public interface IServiceCaller
	{
		IServiceCallState<T> CallService<T>(string extensionName, string commandName, IDictionary<string, object> parameters, HTTPMethod method, bool requiresSession) where T : class, IServiceResult;
	}
}