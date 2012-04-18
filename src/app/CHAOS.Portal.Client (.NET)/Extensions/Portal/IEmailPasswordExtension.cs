using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions.Portal
{
	public interface IEmailPasswordExtension
	{
		IServiceCallState<IServiceResult_Portal<User>> CreatePassword(Guid userGUID, string password);
		IServiceCallState<IServiceResult_Portal<User>> Login(string email, string password);
	}
}