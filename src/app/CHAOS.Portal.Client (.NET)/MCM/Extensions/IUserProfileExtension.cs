using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IUserProfileExtension
	{
		IServiceCallState<PagedResult<UserProfile>> Get(Guid metadataSchemaGuid, Guid? userGuid = null);
		IServiceCallState<PagedResult<UserProfile>> Set(Guid metadataSchemaGuid, XDocument metadata, Guid? userGuid = null);
	}
}