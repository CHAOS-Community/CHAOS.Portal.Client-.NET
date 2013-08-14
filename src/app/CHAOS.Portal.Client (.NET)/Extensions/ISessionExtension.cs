using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface ISessionExtension
	{
		IServiceCallState<PagedResult<Session>> Create();
		IServiceCallState<PagedResult<Session>> Get();
		IServiceCallState<PagedResult<Session>> Update();
		IServiceCallState<PagedResult<ScalarResult>> Delete();
	}
}