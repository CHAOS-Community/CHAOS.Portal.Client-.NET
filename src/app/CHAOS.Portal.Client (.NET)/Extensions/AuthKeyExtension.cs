using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	class AuthKeyExtension : ASessionExtension, IAuthKeyExtension
	{
		public IServiceCallState<PagedResult<Session>> Login(string token)
		{
			return SetSessionUpdatingState(CallService<PagedResult<Session>>(HTTPMethod.POST, token));
		}

		public IServiceCallState<PagedResult<AuthKey>> Create(string name)
		{
			return CallService<PagedResult<AuthKey>>(HTTPMethod.POST, name);
		}

		public IServiceCallState<PagedResult<AuthKey>> Get()
		{
			return CallService<PagedResult<AuthKey>>(HTTPMethod.GET);
		}
		   
		public IServiceCallState<PagedResult<ScalarResult>> Delete(string name)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.POST, name);
		}
	}
}