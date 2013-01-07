using System;
using System.Collections.Generic;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public class SecureCookieExtension : AExtension, ISecureCookieExtension
	{
		public IServiceCallState<SecureCookie> Get()
		{
			return CallService<SecureCookie>(HTTPMethod.GET);
		}

		public IServiceCallState<SecureCookie> Create()
		{
			return CallService<SecureCookie>(HTTPMethod.GET);
		}

		public IServiceCallState<ScalarResult> Delete(IList<Guid> guids)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, guids);
		}

		public IServiceCallState<SecureCookie> Login(Guid guid, Guid securityGUID)
		{
			return CallService<SecureCookie>(HTTPMethod.GET, guid, securityGUID);
		}
	}
}