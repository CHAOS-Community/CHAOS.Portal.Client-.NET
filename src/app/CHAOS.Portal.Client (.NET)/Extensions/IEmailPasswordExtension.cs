using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IEmailPasswordExtension
	{
		IServiceCallState<IServiceResult_EmailPassword<User>> CreatePassword(Guid userGUID, string password);
		IServiceCallState<IServiceResult_EmailPassword<User>> Login(string email, string password);
	}
}