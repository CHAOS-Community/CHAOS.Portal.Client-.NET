using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IUserSettingsExtension
	{
		IServiceCallState<IServiceResult_Portal<UserSetting>> Get(Guid? clientGUID = null);
		IServiceCallState<IServiceResult_Portal<UserSetting>> Set(XElement settings);
		IServiceCallState<IServiceResult_Portal<UserSetting>> Set(Guid? clientGUID, XElement settings);
		IServiceCallState<IServiceResult_Portal<UserSetting>> Delete(Guid? clientGUID = null);
	}
}