using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public class OAuthExtension : AExtension, IOAuthExtension
	{
		public IServiceCallState<PagedResult<User>> Login(string oAuthId, string email, Guid sessionGuidToAuthenticate)
		{
			return CallService<PagedResult<User>>(HTTPMethod.GET, oAuthId, email, sessionGuidToAuthenticate);
		}
	}
}