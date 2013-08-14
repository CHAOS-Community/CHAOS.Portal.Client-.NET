using System.Collections.Generic;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;

namespace CHAOS.Portal.Client.ServiceCall
{
	public interface IServiceCaller
	{
		IServiceCallState<T> CallService<T>(string extensionName, string commandName, IDictionary<string, object> parameters, HTTPMethod method, bool requiresSession) where T : class, IServiceResult;
		void RegisterExtension(IExtension extension);
	}
}