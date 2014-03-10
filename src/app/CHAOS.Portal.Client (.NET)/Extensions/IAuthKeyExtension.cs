using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IAuthKeyExtension
	{
		IServiceCallState<PagedResult<Session>> Login(string token);
		IServiceCallState<PagedResult<AuthKey>> Create(string name);
		IServiceCallState<PagedResult<AuthKey>> Get();
		IServiceCallState<PagedResult<ScalarResult>> Delete(string name);
	}
}