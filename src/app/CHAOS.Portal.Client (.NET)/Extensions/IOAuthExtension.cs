using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IOAuthExtension
	{
		IServiceCallState<PagedResult<User>> Login(string oAuthId, string email, Guid sessionGuidToAuthenticate);
	}
}