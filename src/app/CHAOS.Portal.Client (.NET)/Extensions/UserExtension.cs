using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public class UserExtension : AExtension, IUserExtension
	{
		public IServiceCallState<User> Get()
		{
			return CallService<User>(HTTPMethod.GET);
		}

		public IServiceCallState<User> Create(string firstName, string middleName, string lastName, string email)
		{
			return CallService<User>(HTTPMethod.POST, firstName, middleName, lastName, email);
		}

		public IServiceCallState<ScalarResult> Update(string newFirstName, string newMiddleName, string newLastName, string newEmail)
		{
			return CallService<ScalarResult>(HTTPMethod.POST, newFirstName, newMiddleName, newLastName, newEmail);
		}

		public IServiceCallState<ScalarResult> Delete(Guid guid)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, guid);
		}
	}
}