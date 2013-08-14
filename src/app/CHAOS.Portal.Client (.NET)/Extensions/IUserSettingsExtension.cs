using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IUserSettingsExtension
	{
		IServiceCallState<PagedResult<UserSetting>> Get(Guid? clientGUID = null);
		IServiceCallState<PagedResult<UserSetting>> Set(XElement settings, Guid? clientGUID = null);
		IServiceCallState<PagedResult<UserSetting>> Delete(Guid? clientGUID = null);
	}
}