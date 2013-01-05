using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IExtension
	{
		void Initialize(IServiceCaller portalClient);
	}
}