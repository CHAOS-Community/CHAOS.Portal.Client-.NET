using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class UserExtension : Extension, IUserExtension
	{
		public UserExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }

		public IServiceCallState<IServiceResult_Portal<User>> Get()
		{
			return CallService<IServiceResult_Portal<User>>(HTTPMethod.GET);
		}

		public IServiceCallState<IServiceResult_Portal<User>> Create(string firstName, string middleName, string lastName, string email)
		{
			return CallService<IServiceResult_Portal<User>>(HTTPMethod.POST, firstName, middleName, lastName, email);
		}

		public IServiceCallState<IServiceResult_Portal<ScalarResult>> Update(string newFirstName, string newMiddleName, string newLastName, string newEmail)
		{
			return CallService<IServiceResult_Portal<ScalarResult>>(HTTPMethod.POST, newFirstName, newMiddleName, newLastName, newEmail);
		}

		public IServiceCallState<IServiceResult_Portal<ScalarResult>> Delete(Guid guid)
		{
			return CallService<IServiceResult_Portal<ScalarResult>>(HTTPMethod.GET, guid);
		}
	}
}