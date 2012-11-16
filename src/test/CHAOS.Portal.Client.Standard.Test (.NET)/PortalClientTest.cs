#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;
#endif

using System;

namespace CHAOS.Portal.Client.Standard.Test
{
#if SILVERLIGHT
	[TestClass]
#endif
	public class PortalClientTest : APortalClientUnitTest
	{
#if SILVERLIGHT
		[TestMethod]
#else
		[Test]
#endif
		public void ClientGUIDSetIsRaised()
		{
			var client = GetClient(false);

			var wasRaised = false;

			client.ClientGUIDSet += (sender, args) => wasRaised = true;

			client.ClientGUID = Guid.NewGuid();

			Assert.IsTrue(wasRaised);
		}
	}
}