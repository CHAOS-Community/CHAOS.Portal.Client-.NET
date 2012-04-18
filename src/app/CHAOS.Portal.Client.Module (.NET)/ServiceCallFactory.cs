using CHAOS.Common.Utilities;
using CHAOS.Portal.Client.Data;
using Ninject;

namespace CHAOS.Portal.Client.Standard.ServiceCall
{
	public class ServiceCallFactory : IServiceCallFactory
	{
		private readonly IKernel _Kernel;

		public ServiceCallFactory(IKernel kernel)
		{
			_Kernel = ArgumentUtilities.ValidateIsNotNull("kernel", kernel);
		}

		public IServiceCall<T> GetServiceCall<T>() where T : class, IServiceResult
		{
			return _Kernel.Get<IServiceCall<T>>();
		}
	}
}