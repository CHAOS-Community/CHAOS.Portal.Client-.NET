using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public class SessionExtension : ASessionExtension, ISessionExtension
	{
		public IServiceCallState<PagedResult<Session>> Create()
		{
			return SetSessionUpdatingState(CallServiceWithoutSession <PagedResult<Session>>(HTTPMethod.GET));
		}

		public IServiceCallState<PagedResult<Session>> Get()
		{
			return CallService<PagedResult<Session>>(HTTPMethod.GET);
		}

		public IServiceCallState<PagedResult<Session>> Update()
		{
			return SetSessionUpdatingState(CallService<PagedResult<Session>>(HTTPMethod.POST));
		}

		public IServiceCallState<PagedResult<ScalarResult>> Delete()
		{
			return SetSessionDeletingState(CallService<PagedResult<ScalarResult>>(HTTPMethod.GET));
		}
	}
}