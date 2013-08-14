using System;
using System.Collections.Generic;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface ISecureCookieExtension
	{
		IServiceCallState<PagedResult<SecureCookie>> Get();
		IServiceCallState<PagedResult<SecureCookie>> Create();
		IServiceCallState<PagedResult<ScalarResult>> Delete(IList<Guid> guids);
		IServiceCallState<PagedResult<SecureCookie>> Login(Guid guid, Guid securityGUID);
	}
}