using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Utilities;
using Ninject;

namespace CHAOS.Portal.Client.Module
{
	public class ServiceCallFactory : IServiceCallFactory
	{
		private readonly IKernel _kernel;

		public ServiceCallFactory(IKernel kernel)
		{
			_kernel = ArgumentUtilities.ValidateIsNotNull("kernel", kernel);
		}

		public IServiceCall<T> GetServiceCall<T>() where T : class, IServiceBody
		{
			return _kernel.Get<IServiceCall<T>>();
		}
	}
}