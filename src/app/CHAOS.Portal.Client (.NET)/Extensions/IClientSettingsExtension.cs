using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IClientSettingsExtension
	{
		IServiceCallState<ClientSettings> Get(Guid guid);
	}
}