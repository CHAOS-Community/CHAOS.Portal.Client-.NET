using System;
using System.Collections.Generic;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface ISecureCookieExtension
	{
		IServiceCallState<IServiceResult_Portal<SecureCookie>> Get();
		IServiceCallState<IServiceResult_Portal<SecureCookie>> Create();
		IServiceCallState<IServiceResult_Portal<ScalarResult>> Delete(IList<Guid> guids);
		IServiceCallState<IServiceResult_Portal<SecureCookie>> Login(Guid guid, Guid securityGUID);
	}
}