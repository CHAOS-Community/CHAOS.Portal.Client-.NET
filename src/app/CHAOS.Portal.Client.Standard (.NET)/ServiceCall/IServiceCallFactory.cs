using CHAOS.Portal.Client.Data;

namespace CHAOS.Portal.Client.Standard.ServiceCall
{
	public interface IServiceCallFactory
	{
		IServiceCall<T> GetServiceCall<T>() where T : class, IServiceBody;
	}
}