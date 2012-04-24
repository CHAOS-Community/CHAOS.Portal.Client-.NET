using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
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
		public const string SERVICE_PATH = "http://api.chaos-systems.com";

#if SILVERLIGHT
		[TestMethod, Asynchronous, Timeout(5000)]
#else
		[Test]
#endif
		public void ShouldCallSessionCreate()
		{
			var kernel = new StandardKernel();

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