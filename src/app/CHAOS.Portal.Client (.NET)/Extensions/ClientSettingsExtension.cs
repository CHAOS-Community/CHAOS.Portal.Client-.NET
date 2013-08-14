using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public class ClientSettingsExtension : AExtension, IClientSettingsExtension
	{
		public IServiceCallState<PagedResult<ClientSettings>> Get(Guid guid)
		{
			return CallService<PagedResult<ClientSettings>>(HTTPMethod.GET, guid);
		}

		public IServiceCallState<PagedResult<ScalarResult>> Set(Guid guid, string name, XElement settings)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.GET, guid, name, settings);
		}
	}
}