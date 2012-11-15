using System;
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

		private static IPortalClient _loggedInClient;

		public static IPortalClient Getclient(bool createSession = true, bool login = true)
		{
			if(!createSession && login)
				throw new InvalidOperationException("Cannot login without creating session");
			
			if (createSession && login && _loggedInClient != null)
				return _loggedInClient;
			
			var kernel = new StandardKernel();

			kernel.Bind<IStringSerializer>().To<StringSerializer>().InSingletonScope();
			kernel.Bind<IXMLSerializer>().To<XMLSerializer>().InSingletonScope();
			kernel.Get<IXMLSerializer>().Map(typeof(IList<>), typeof(List<>));

			kernel.Load(new Module.Module());

			var client = kernel.Get<IPortalClient>();

			if (USE_LATEST)
				((PortalClient) client).UseLatest = true;

			client.ServicePath = SERVICE_PATH;

			if (createSession)
				CreateSession(client);

			if (login)
			{
				Login(client);
				_loggedInClient = client;
			}

			return client;
		}

		public static IPortalClient CreateSession(IPortalClient client)
		{
			client.Session.Create().Synchronous(CALL_TIMEOUT).ThrowFirstError();

			return client;
		}

		public static IPortalClient Login(IPortalClient client)
		{
			client.EmailPassword.Login(LOGIN_EMAIL, LOGIN_PASSWORD).Synchronous(CALL_TIMEOUT).ThrowFirstError();

			return client;
		}
	}
}