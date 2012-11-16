using System.Collections.Generic;
using CHAOS.Serialization.Standard.String;
using CHAOS.Serialization.Standard.XML;
using CHAOS.Serialization.String;
using CHAOS.Serialization.XML;
using Ninject;

namespace CHAOS.Portal.Client.Standard.Test
{
	public static class PortalClientTestHelper
	{
		private const string SERVICE_PATH = "";
		public const string LOGIN_EMAIL = "";
		public const string LOGIN_PASSWORD = "";
		private const bool USE_LATEST = true;
		public const uint CALL_TIMEOUT = uint.MaxValue;

		public static IPortalClient GetClient()
		{	
			var kernel = new StandardKernel();

			kernel.Bind<IStringSerializer>().To<StringSerializer>().InSingletonScope();
			kernel.Bind<IXMLSerializer>().To<XMLSerializer>().InSingletonScope();
			kernel.Get<IXMLSerializer>().Map(typeof(IList<>), typeof(List<>));

			kernel.Load(new Module.Module());

			var client = kernel.Get<IPortalClient>();

			if (USE_LATEST)
				((PortalClient) client).UseLatest = true;

			client.ServicePath = SERVICE_PATH;

			return client;
		}
	}
}