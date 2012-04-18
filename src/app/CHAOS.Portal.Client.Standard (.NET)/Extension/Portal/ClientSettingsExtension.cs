using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.Extensions.Portal;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension.Portal
{
	public class ClientSettingsExtension : Extension, IClientSettingsExtension
	{
		public ClientSettingsExtension(IServiceCaller serviceCaller) : base(serviceCaller)
		{
		}

		public IServiceCallState<IServiceResult_Portal<ClientSettings>> Get(Guid guid)
		{
			return CallService<IServiceResult_Portal<ClientSettings>>(HTTPMethod.GET, guid);
		}
	}
}