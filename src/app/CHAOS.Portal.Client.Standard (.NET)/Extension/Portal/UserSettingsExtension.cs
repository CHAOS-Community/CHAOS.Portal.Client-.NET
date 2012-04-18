using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.Extensions.Portal;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension.Portal
{
	public class UserSettingsExtension : Extension, IUserSettingsExtension
	{
		private readonly IPortalClient _PortalClient;

		public UserSettingsExtension(IServiceCaller serviceCaller, IPortalClient portalClient) : base(serviceCaller)
		{
			_PortalClient = portalClient;
		}

		public IServiceCallState<IServiceResult_Portal<UserSetting>> Get(Guid? clientGUID)
		{
			if(!clientGUID.HasValue)
			{
				if (_PortalClient.HasClientGUID)
					clientGUID = _PortalClient.ClientGUID;
				else
					throw new InvalidOperationException("Guid must be set on IPortalClient or method call");
			}
			
			return CallService<IServiceResult_Portal<UserSetting>>(HTTPMethod.GET, clientGUID);
		}

		public IServiceCallState<IServiceResult_Portal<UserSetting>> Set(XElement settings)
		{
			return Set(null, settings);
		}

		public IServiceCallState<IServiceResult_Portal<UserSetting>> Set(Guid? clientGUID, XElement settings)
		{
			if (!clientGUID.HasValue)
			{
				if (_PortalClient.HasClientGUID)
					clientGUID = _PortalClient.ClientGUID;
				else
					throw new InvalidOperationException("Guid must be set on IPortalClient or method call");
			}

			return CallService<IServiceResult_Portal<UserSetting>>(HTTPMethod.POST, clientGUID, settings);
		}

		public IServiceCallState<IServiceResult_Portal<UserSetting>> Delete(Guid? clientGUID)
		{
			if (!clientGUID.HasValue)
			{
				if (_PortalClient.HasClientGUID)
					clientGUID = _PortalClient.ClientGUID;
				else
					throw new InvalidOperationException("Guid must be set on IPortalClient or method call");
			}

			return CallService<IServiceResult_Portal<UserSetting>>(HTTPMethod.GET, clientGUID);
		}
	}
}