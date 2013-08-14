using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IEmailPasswordExtension
	{
		IServiceCallState<PagedResult<User>> CreatePassword(Guid userGUID, string password);
		IServiceCallState<PagedResult<User>> Login(string email, string password);
	}
}