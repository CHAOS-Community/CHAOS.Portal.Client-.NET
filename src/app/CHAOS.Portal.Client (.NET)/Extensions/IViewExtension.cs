using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IViewExtension
	{
		IServiceCallState<T> Get<T>(string name) where T : class;
	}
}