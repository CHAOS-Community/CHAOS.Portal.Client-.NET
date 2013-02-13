using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public class ViewExtension : AExtension, IViewExtension
	{
		public IServiceCallState<T> Get<T>(string name) where T : class
		{
			return CallService<T>(HTTPMethod.GET, name);
		}
	}
}