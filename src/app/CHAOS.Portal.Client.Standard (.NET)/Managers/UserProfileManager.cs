using System;
using CHAOS.Extensions;
using CHAOS.Portal.Client.Managers;
using CHAOS.Portal.Client.MCM.Extensions;
using CHAOS.Serialization.XML;

namespace CHAOS.Portal.Client.Standard.Managers
{
	public class UserProfileManager : IUserProfileManager
	{
		private readonly IXMLSerializer _serializer;
		private readonly IPortalClient _client;

		public UserProfileManager(IXMLSerializer serializer, IPortalClient client)
		{
			_serializer = serializer;
			_client = client;
		}

		public void Get<T>(Action<bool, T> callback, Guid metadataSchemaGuid, Guid? userGuid = null) where T : class
		{
			callback.ValidateIsNotNull("callback");

			_client.UserProfile().Get(metadataSchemaGuid, userGuid).Callback = (response, token) =>
			{
				if (response.Error != null)
				{
					callback(false, null);
					return;
				}

				if (response.Body.Count == 0 || response.Body.Results[0].MetadataXml == null)
					callback(true, null);

				try
				{
					callback(true, _serializer.Deserialize<T>(response.Body.Results[0].MetadataXml, false));
				}
				catch (Exception)
				{
					callback(false, null);
				}
			};
		}
	}
}