using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class EmailPasswordExtension : AExtension, IEmailPasswordExtension
	{
		public EmailPasswordExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }

		public IServiceCallState<IServiceResult_Portal<User>> CreatePassword(Guid userGUID, string password)
		{
			return CallService<IServiceResult_Portal<User>>(HTTPMethod.GET, userGUID, password);
		}

		public IServiceCallState<IServiceResult_Portal<User>> Login(string email, string password)
		{
			return CallService<IServiceResult_Portal<User>>(HTTPMethod.GET, email, password);
		}
	}
}