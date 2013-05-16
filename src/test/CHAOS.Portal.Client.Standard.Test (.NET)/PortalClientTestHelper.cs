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
		public static string ServicePath = "";
		public static string LoginEmail = "";
		public static string LoginPassword = "";
		public static bool UseLatest = false;
		public const uint CALL_TIMEOUT = 5000;

		public static IPortalClient GetClient()
		{	
			var kernel = new StandardKernel();

			kernel.Bind<IStringSerializer>().To<StringSerializer>().InSingletonScope();
			kernel.Bind<IXMLSerializer>().To<XMLSerializer>().InSingletonScope();
			kernel.Get<IXMLSerializer>().Map(typeof(IList<>), typeof(List<>));

			kernel.Load(new Module.Module());

			var client = kernel.Get<IPortalClient>();

			if (UseLatest)
				((PortalClient) client).UseLatest = true;

			client.ServicePath = ServicePath;

			return client;
		}
	}
}