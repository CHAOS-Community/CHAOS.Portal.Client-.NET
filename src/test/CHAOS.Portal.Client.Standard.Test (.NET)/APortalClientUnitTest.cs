using CHAOS.Portal.Client.Data;
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

		protected Func<IList<T>> CallPortalWithPagedResult<T>(Func<IPortalClient, IServiceCallState<PagedResult<T>>> caller, bool createSession = true, bool login = true, bool reuseClient = true) where T : class 
		{
			var client = GetClient(createSession, login, reuseClient);

			return CallPortalWithPagedResult(() => caller(client));
		}

		protected Func<IList<T>> CallPortalWithPagedResult<T>(Func<IServiceCallState<PagedResult<T>>> caller) where T : class
		{
			var data = CallPortal(caller);

			return () => data().Results;
		 }

		protected Func<T> CallPortal<T>(Func<IPortalClient, IServiceCallState<T>> caller, bool createSession = true, bool login = true, bool reuseClient = true) where T : class, IServiceResult
		{
			var client = GetClient(createSession, login, reuseClient);

			return CallPortal(() => caller(client));
		}

		protected Func<T> CallPortal<T>(Func<IServiceCallState<T>> caller) where T : class, IServiceResult
		{
			T result = null;
#if SILVERLIGHT
			IServiceCallState<T> state = null;

			EnqueueCallback(() => state = caller());
			EnqueueConditional(() => state.Response != null);
			EnqueueCallback(() => result = state.ThrowError().Response.Body);
#else
			result = caller().Synchronous(PortalClientTestHelper.CALL_TIMEOUT).ThrowError().Response.Body;
#endif
			return () => result;
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
				CallPortalWithPagedResult(() => client.Session().Create());

			if (createSession && login)
				CallPortalWithPagedResult(() => client.EmailPassword().Login(PortalClientTestHelper.LoginEmail, PortalClientTestHelper.LoginPassword));

			return client;
		}
	}
}