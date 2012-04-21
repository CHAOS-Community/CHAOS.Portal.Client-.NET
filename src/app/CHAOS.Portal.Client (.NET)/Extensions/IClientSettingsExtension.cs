using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IClientSettingsExtension
	{
		IServiceCallState<IServiceResult_Portal<ClientSettings>> Get(Guid guid);
	}
}