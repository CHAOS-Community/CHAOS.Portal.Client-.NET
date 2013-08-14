using System;
using System.Collections.Generic;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public class SecureCookieExtension : AExtension, ISecureCookieExtension
	{
		public IServiceCallState<PagedResult<SecureCookie>> Get()
		{
			return CallService<PagedResult<SecureCookie>>(HTTPMethod.GET);
		}

		public IServiceCallState<PagedResult<SecureCookie>> Create()
		{
			return CallService<PagedResult<SecureCookie>>(HTTPMethod.GET);
		}

		public IServiceCallState<PagedResult<ScalarResult>> Delete(IList<Guid> guids)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.GET, guids);
		}

		public IServiceCallState<PagedResult<SecureCookie>> Login(Guid guid, Guid securityGUID)
		{
			return CallService<PagedResult<SecureCookie>>(HTTPMethod.GET, guid, securityGUID);
		}
	}
}