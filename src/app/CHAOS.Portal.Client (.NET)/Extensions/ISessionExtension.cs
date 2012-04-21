using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface ISessionExtension
	{
		IServiceCallState<IServiceResult_Portal<Session>> Create();
		IServiceCallState<IServiceResult_Portal<Session>> Get();
		IServiceCallState<IServiceResult_Portal<Session>> Update();
		IServiceCallState<IServiceResult_Portal<ScalarResult>> Delete();
	}
}