using System;
using System.Collections.Generic;
using Geckon.Common.Utilities;
using Geckon.Portal.Client.Data;
using Geckon.Portal.Client.Data.Portal;
using Geckon.Serialization.Standard.String;
using Geckon.Serialization.Standard.XML;
using Geckon.Serialization.String;
using Geckon.Serialization.XML;
using Ninject;

#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;
#endif

namespace Geckon.Portal.Client.Standard.Test
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

			kernel.Get<IXMLSerializer>().Map(typeof (IList<>), typeof (List<>));

			kernel.Load(new Module());

			var client = kernel.Get<IPortalClient>();

			client.ServicePath = "http://services.web01.geckon.com/Portal_v3";

			IServiceResult_Portal<Session> result = null;

			Func<bool> condition = () => result != null;
			Action asserts = () =>
			                 	{
			                 		Assert.IsNotNull(result.Portal.Data[0]);
			                 		Assert.IsNotNull(result.Portal.Data[0].SessionID);
			                 		Assert.AreNotEqual(string.Empty, result.Portal.Data[0].SessionID);
			                 	};

			client.Session.Create().Callback = (r, e, t) => result = r;

#if SILVERLIGHT
			EnqueueConditional(condition);
			EnqueueCallback(asserts);
			EnqueueTestComplete();
#else
			Timing.WaitUntil(condition, 5000, "client.Session.Create call timed out");
			asserts();
#endif
		}
	}
}