using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IUserExtension
	{
		IServiceCallState<User> Get();
		IServiceCallState<User> Create(string firstName, string middleName, string lastName, string email);
		IServiceCallState<ScalarResult> Update(string newFirstName, string newMiddleName, string newLastName, string newEmail);
		IServiceCallState<ScalarResult> Delete(Guid guid);
	}
}