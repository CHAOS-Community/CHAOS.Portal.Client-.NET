using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public class UserExtension : AExtension, IUserExtension
	{
		public IServiceCallState<PagedResult<User>> Get()
		{
			return CallService<PagedResult<User>>(HTTPMethod.GET);
		}

		public IServiceCallState<PagedResult<User>> Create(string firstName, string middleName, string lastName, string email)
		{
			return CallService<PagedResult<User>>(HTTPMethod.POST, firstName, middleName, lastName, email);
		}

		public IServiceCallState<PagedResult<ScalarResult>> Update(string newFirstName, string newMiddleName, string newLastName, string newEmail)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.POST, newFirstName, newMiddleName, newLastName, newEmail);
		}

		public IServiceCallState<PagedResult<ScalarResult>> Delete(Guid guid)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.GET, guid);
		}
	}
}