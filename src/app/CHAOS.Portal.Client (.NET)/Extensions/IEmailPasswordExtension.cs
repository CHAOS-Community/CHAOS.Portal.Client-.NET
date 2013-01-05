using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IEmailPasswordExtension
	{
		IServiceCallState<User> CreatePassword(Guid userGUID, string password);
		IServiceCallState<User> Login(string email, string password);
	}
}