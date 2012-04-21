using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.GeoLocator;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface ILocationExtension
	{
		IServiceCallState<IServiceResult_GeoLocator<LocationInfo>> Get(string ip = null);
	}
}