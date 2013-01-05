using System;
using System.Collections.Generic;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface ISecureCookieExtension
	{
		IServiceCallState<SecureCookie> Get();
		IServiceCallState<SecureCookie> Create();
		IServiceCallState<ScalarResult> Delete(IList<Guid> guids);
		IServiceCallState<SecureCookie> Login(Guid guid, Guid securityGUID);
	}
}