using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IUserExtension
	{
		IServiceCallState<IServiceResult_Portal<User>> Get();
		IServiceCallState<IServiceResult_Portal<User>> Create(string firstName, string middleName, string lastName, string email);
		IServiceCallState<IServiceResult_Portal<ScalarResult>> Update(string newFirstName, string newMiddleName, string newLastName, string newEmail);
		IServiceCallState<IServiceResult_Portal<ScalarResult>> Delete(Guid guid);
	}
}