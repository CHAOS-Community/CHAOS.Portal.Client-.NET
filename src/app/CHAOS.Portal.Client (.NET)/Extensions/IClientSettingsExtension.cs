using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IClientSettingsExtension
	{
		IServiceCallState<PagedResult<ClientSettings>> Get(Guid guid);
		IServiceCallState<PagedResult<ScalarResult>> Set(Guid guid, string name, XElement settings);
	}
}