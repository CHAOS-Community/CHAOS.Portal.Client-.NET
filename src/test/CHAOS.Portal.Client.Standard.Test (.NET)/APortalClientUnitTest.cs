#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;
#endif

using System;
using System.Collections.Generic;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Extensions;

namespace CHAOS.Portal.Client.Standard.Test
{
#if !SILVERLIGHT
	[TestFixture]
#endif
	public abstract class APortalClientUnitTest
#if SILVERLIGHT
		: SilverlightTest
#endif
	{
		private static IPortalClient _reusableClient;

		protected Func<IList<T>> CallPortal<T>(Func<IPortalClient, IServiceCallState<T>> caller, bool createSession = true, bool login = true, bool reuseClient = true) where T : class
		{
			var client = GetClient(createSession, login, reuseClient);

			return CallPortal(() => caller(client));
		}

		protected Func<IList<T>> CallPortal<T>(Func<IServiceCallState<T>> caller) where T : class
		{
			IList<T> data = null;
#if SILVERLIGHT
			 IServiceCallState<T> state = null;

			 EnqueueCallback(() => state = caller());
			 EnqueueConditional(() => state.Response != null);
			 EnqueueCallback(() => data = state.ThrowError().Response.Result.Results);
#else
			 data = caller().Synchronous(PortalClientTestHelper.CALL_TIMEOUT).ThrowError().Response.Result.Results;
#endif
			 return () => data;
		 }

		protected void Test(Action test)
		{
#if SILVERLIGHT
			EnqueueCallback(test);
#else
			test();
#endif
		}

		protected void TestData<T>(Func<T> data, Action<T> test) where T : class
		{
#if SILVERLIGHT
			EnqueueCallback(() => test(data()));
#else
			test(data());
#endif
		}

		protected void EndTest()
		{
#if SILVERLIGHT
			EnqueueTestComplete();
#endif
		}

		protected IPortalClient GetClient(bool createSession = true, bool login = true, bool reuseClient = true)
		{
			if (createSession && login && reuseClient && _reusableClient != null)
				return _reusableClient;

			var client = PortalClientTestHelper.GetClient();

			if (createSession && login && reuseClient && _reusableClient != null)
				_reusableClient = client;

			if (createSession)
				CallPortal(() => client.Session().Create());

			if (createSession && login)
				CallPortal(() => client.EmailPassword().Login(PortalClientTestHelper.LoginEmail, PortalClientTestHelper.LoginPassword));

			return client;
		}
	}
}