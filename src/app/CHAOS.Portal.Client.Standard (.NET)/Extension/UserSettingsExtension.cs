using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class UserSettingsExtension : AExtension, IUserSettingsExtension
	{
		private readonly IPortalClient _portalClient;

		public UserSettingsExtension(IServiceCaller serviceCaller, IPortalClient portalClient) : base(serviceCaller)
		{
			_portalClient = portalClient;
		}

		public IServiceCallState<IServiceResult_Portal<UserSetting>> Get(Guid? clientGUID)
		{
			if(!clientGUID.HasValue)
			{
				if (_portalClient.HasClientGUID)
					clientGUID = _portalClient.ClientGUID;
				else
					throw new InvalidOperationException("Guid must be set on IPortalClient or method call");
			}
			
			return CallService<IServiceResult_Portal<UserSetting>>(HTTPMethod.GET, clientGUID);
		}


		public IServiceCallState<IServiceResult_Portal<UserSetting>> Set(XElement settings, Guid? clientGUID)
		{
			if (!clientGUID.HasValue)
			{
				if (_portalClient.HasClientGUID)
					clientGUID = _portalClient.ClientGUID;
				else
					throw new InvalidOperationException("Guid must be set on IPortalClient or method call");
			}

			return CallService<IServiceResult_Portal<UserSetting>>(HTTPMethod.POST, clientGUID, settings);
		}

		public IServiceCallState<IServiceResult_Portal<UserSetting>> Delete(Guid? clientGUID)
		{
			if (!clientGUID.HasValue)
			{
				if (_portalClient.HasClientGUID)
					clientGUID = _portalClient.ClientGUID;
				else
					throw new InvalidOperationException("Guid must be set on IPortalClient or method call");
			}

			return CallService<IServiceResult_Portal<UserSetting>>(HTTPMethod.GET, clientGUID);
		}
	}
}