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
#else
	[TestFixture]
#endif
	public class PortalClientTest
#if SILVERLIGHT
		: SilverlightTest
#endif
	{
#if SILVERLIGHT
		[TestMethod]
#else
		[Test]
#endif
		public void ClientGUIDSetIsRaised()
		{
			var client = PortalClientTestHelper.Getclient(false, false);

			var wasRaised = false;

			client.ClientGUIDSet += (sender, args) => wasRaised = true;

			client.ClientGUID = Guid.NewGuid();

			Assert.IsTrue(wasRaised);
		}
	}
}