using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public class ClientSettingsExtension : AExtension, IClientSettingsExtension
	{
		public IServiceCallState<ClientSettings> Get(Guid guid)
		{
			return CallService<ClientSettings>(HTTPMethod.GET, guid);
		}
	}
}