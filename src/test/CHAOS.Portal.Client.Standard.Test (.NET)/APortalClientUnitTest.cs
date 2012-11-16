#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;
#endif

using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

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

		protected Func<T> CallPortal<T>(Func<IPortalClient, IServiceCallState<T>> caller, bool createSession = true, bool login = true, bool reuseClient = true) where T : class, IServiceResult
		{
			var client = GetClient(createSession, login, reuseClient);

			return CallPortal(() => caller(client));
		}

		protected Func<T> CallPortal<T>(Func<IServiceCallState<T>> caller) where T : class, IServiceResult
		{
#if SILVERLIGHT
			 IServiceCallState<T> state = null;
			 T data = null;

			 EnqueueCallback(() => state = caller());
			 EnqueueConditional(() => state.Result != null);
			 EnqueueCallback(() => data = state.ThrowFirstError().Result);

			 return () => data;
#else
			 return () => caller().Synchronous(PortalClientTestHelper.CALL_TIMEOUT).ThrowFirstError().Result;
#endif
		 }

		protected void Test(Action test)
		{
#if SILVERLIGHT
			EnqueueCallback(test);
#else
			test();
#endif
		}

		protected void TestData<T>(Func<T> data, Action<T> test) where T : class, IServiceResult
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
				CallPortal(() => client.Session.Create());

			if (createSession && login)
				CallPortal(() => client.EmailPassword.Login(PortalClientTestHelper.LOGIN_EMAIL, PortalClientTestHelper.LOGIN_PASSWORD));

			return client;
		}
	}
}