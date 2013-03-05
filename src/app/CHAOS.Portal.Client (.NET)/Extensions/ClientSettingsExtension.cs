using System;
using System.Xml.Linq;
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

		public IServiceCallState<ScalarResult> Set(Guid guid, string name, XElement settings)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, guid, name, settings);
		}
	}
}