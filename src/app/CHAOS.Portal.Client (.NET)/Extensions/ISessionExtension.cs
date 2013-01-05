using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface ISessionExtension
	{
		IServiceCallState<Session> Create();
		IServiceCallState<Session> Get();
		IServiceCallState<Session> Update();
		IServiceCallState<ScalarResult> Delete();
	}
}