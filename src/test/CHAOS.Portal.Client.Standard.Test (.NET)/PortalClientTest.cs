using System;
using System.Collections.Generic;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Indexing;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Serialization.Standard.String;
using CHAOS.Serialization.Standard.XML;
using CHAOS.Serialization.String;
using CHAOS.Serialization.XML;
using CHAOS.Tasks;
using Object = CHAOS.Portal.Client.Data.MCM.Object;
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
		private const string SERVICE_PATH = "";
		private const string LOGIN_EMAIL = "";
		private const string LOGIN_PASSWORD = "";
		public const uint CALL_TIMEOUT = uint.MaxValue;

		public static IPortalClient Getclient(bool createSession = false, bool login = false)
		{
			var kernel = new StandardKernel();

			kernel.Bind<IStringSerializer>().To<StringSerializer>().InSingletonScope();
			kernel.Bind<IXMLSerializer>().To<XMLSerializer>().InSingletonScope();
			kernel.Get<IXMLSerializer>().Map(typeof(IList<>), typeof(List<>));

			kernel.Load(new Module.Module());

			var client = kernel.Get<IPortalClient>();

			client.ServicePath = SERVICE_PATH;

			if (createSession)
				CreateSession(client);

			if (login)
				Login(client);

			return client;
		}

		public static IPortalClient CreateSession(IPortalClient client)
		{
			var state = client.Session.Create().Synchronous(CALL_TIMEOUT);

			if (state.Error != null)
				throw state.Error;
			if (state.Result.Portal.Error != null)
				throw state.Result.Portal.Error;

			return client;
		}

		public static IPortalClient Login(IPortalClient client)
		{
			var state = client.EmailPassword.Login(LOGIN_EMAIL, LOGIN_PASSWORD).Synchronous(CALL_TIMEOUT);

			if (state.Error != null)
				throw state.Error;
			if (state.Result.EmailPassword.Error != null)
				throw state.Result.EmailPassword.Error;

			return client;
		}

#if SILVERLIGHT
		[TestMethod, Asynchronous, Timeout(5000)]
#else
		[Test]
#endif
		public void ShouldCallSessionCreate()
		{
			var client = Getclient();

			IServiceResult_Portal<Session> result = null;

			Func<bool> condition = () => result != null;

			Action asserts = () =>
			                 	{
									Assert.AreNotEqual(0, result.Portal.Data.Count, "No session info returned");
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
			Timing.WaitUntil(condition, 500000, 100,  "client.Session.Create call timed out");
			asserts();
#endif
		}

#if SILVERLIGHT
		[TestMethod, Asynchronous, Timeout(5000)]
#else
		[Test]
#endif
		public void ShouldCallObjectGet()
		{
			var client = Getclient();

			IServiceResult_MCM<Object> result = null;

			Func<bool> condition = () => result != null;

			Action asserts = () =>
			{
				Assert.IsNotNull(result.MCM.Data[0]);
			};

			client.Session.Create().Callback = (r, e, t) =>
			                                   	{
			                                   		if(e != null)
														Assert.Fail(e.Message);
			                                   		else
			                                   		{
			                                   			client.Object.Get("", null, 0, 1, true, true, true, true).Callback = (r2, e2, t2) => result = r2; //TODO: Update search string.
			                                   		}
			                                   	};

#if SILVERLIGHT
			EnqueueConditional(condition);
			EnqueueCallback(asserts);
			EnqueueTestComplete();
#else
			Timing.WaitUntil(condition, 5000, 100, "client.Session.Create call timed out");
			asserts();
#endif
		}

#if SILVERLIGHT
		[TestMethod, Asynchronous, Timeout(5000)]
#else
		[Test]
#endif
		public void ShouldDoFacetSearch()
		{
			var client = Getclient();

			IServiceResult_Index<IndexResponse> result = null;

			Func<bool> condition = () => result != null;

			Action asserts = () =>
			{
				Assert.IsNotNull(result.Index.Data[0]);
			};

			client.Session.Create().Callback = (r, e, t) =>
			{
				if (e != null)
					Assert.Fail(e.Message);
				else
					client.Index.Search(null, "ObjectTypeID:1", null, 0, 1).Callback = (r2, e2, t2) => result = r2; //TODO: Update search string.
			};

#if SILVERLIGHT
			EnqueueConditional(condition);
			EnqueueCallback(asserts);
			EnqueueTestComplete();
#else
			Timing.WaitUntil(condition, 5000, 100, "client.Session.Create call timed out");
			asserts();
#endif
		}
	}
}