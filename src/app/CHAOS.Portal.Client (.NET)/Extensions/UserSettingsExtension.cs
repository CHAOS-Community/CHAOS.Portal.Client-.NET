using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public class UserSettingsExtension : AExtension, IUserSettingsExtension, IClientGUIDDependentExtension
	{
		public Guid ClientGUID { set; private get; }

		public IServiceCallState<UserSetting> Get(Guid? clientGUID)
		{
			if(!clientGUID.HasValue)
				clientGUID = ClientGUID;
			
			return CallService<UserSetting>(HTTPMethod.GET, clientGUID);
		}


		public IServiceCallState<UserSetting> Set(XElement settings, Guid? clientGUID)
		{
			if (!clientGUID.HasValue)
				clientGUID = ClientGUID;

			return CallService<UserSetting>(HTTPMethod.POST, settings, clientGUID);
		}

		public IServiceCallState<UserSetting> Delete(Guid? clientGUID)
		{
			if (!clientGUID.HasValue)
				clientGUID = ClientGUID;

			return CallService<UserSetting>(HTTPMethod.GET, clientGUID);
		}
	}
}