using System;
using System.Collections.Generic;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class SecureCookieExtension : Extension, ISecureCookieExtension
	{
		public SecureCookieExtension(IServiceCaller serviceCaller) : base(serviceCaller)
		{
		}

		public IServiceCallState<IServiceResult_Portal<SecureCookie>> Get()
		{
			return CallService<IServiceResult_Portal<SecureCookie>>(HTTPMethod.GET);
		}

		public IServiceCallState<IServiceResult_Portal<SecureCookie>> Create()
		{
			return CallService<IServiceResult_Portal<SecureCookie>>(HTTPMethod.GET);
		}

		public IServiceCallState<IServiceResult_Portal<ScalarResult>> Delete(IList<Guid> guids)
		{
			return CallService<IServiceResult_Portal<ScalarResult>>(HTTPMethod.GET, guids);
		}

		public IServiceCallState<IServiceResult_Portal<SecureCookie>> Login(Guid guid, Guid securityGUID)
		{
			return CallService<IServiceResult_Portal<SecureCookie>>(HTTPMethod.GET, guid, securityGUID);
		}
	}
}