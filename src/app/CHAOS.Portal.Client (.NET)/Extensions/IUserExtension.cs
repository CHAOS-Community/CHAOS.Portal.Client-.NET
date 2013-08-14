using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IUserExtension
	{
		IServiceCallState<PagedResult<User>> Get();
		IServiceCallState<PagedResult<User>> Create(string firstName, string middleName, string lastName, string email);
		IServiceCallState<PagedResult<ScalarResult>> Update(string newFirstName, string newMiddleName, string newLastName, string newEmail);
		IServiceCallState<PagedResult<ScalarResult>> Delete(Guid guid);
	}
}