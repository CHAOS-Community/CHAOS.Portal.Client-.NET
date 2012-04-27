using System;
using System.Collections.Generic;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Serialization.Standard.String;
using CHAOS.Serialization.Standard.XML;
using CHAOS.Serialization.String;
using CHAOS.Serialization.XML;
using CHAOS.Utilities;
using Ninject;

#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;
#endif

namespace CHAOS.Portal.Client.Standard.Test
{
#if SILVERLIGHT
	[TestClass]
#else
	[TestFixture]
#endif
	public class PortalClientTest
#if SILVERLIGHT
		: SilverlightTest
#endif
	{
		public const string SERVICE_PATH = "";

#if SILVERLIGHT
		[TestMethod, Asynchronous, Timeout(5000)]
#else
		[Test]
#endif
		public void ShouldCallSessionCreate()
		{
			var kernel = new StandardKernel();

			kernel.Bind<IStringSerializer>().To<StringSerializer>().InSingletonScope();
			kernel.Bind<IXMLSerializer>().To<XMLSerializer>().InSingletonScope();
			kernel.Get<IXMLSerializer>().Map(typeof(IList<>), typeof(List<>));

			kernel.Load(new Module.Module());

			var client = kernel.Get<IPortalClient>();

			client.ServicePath = SERVICE_PATH;

			IServiceResult_Portal<Session> result = null;

			Func<bool> condition = () => result != null;

			Action asserts = () =>
			                 	{
			                 		Assert.IsNotNull(result.Portal.Data[0]);
			                 		Assert.IsNotNull(result.Portal.Data[0].SessionGUID);
			                 		Assert.AreNotEqual(string.Empty, result.Portal.Data[0].SessionGUID);
			                 	};

			client.Session.Create().Callback = (r, e, t) => result = r;

#if SILVERLIGHT
			EnqueueConditional(condition);
			EnqueueCallback(asserts);
			EnqueueTestComplete();
#else
			Timing.WaitUntil(condition, 5000, 100,  "client.Session.Create call timed out");
			asserts();
#endif
		}
	}
}