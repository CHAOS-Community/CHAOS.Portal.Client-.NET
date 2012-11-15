using System;
using NUnit.Framework;
#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else

#endif

namespace CHAOS.Portal.Client.Standard.Test.Extensions
{
#if SILVERLIGHT
	[TestClass]
#else
	[TestFixture]
#endif
	public class SessionExtensionTest
#if SILVERLIGHT
		: SilverlightTest
#endif
	{
#if SILVERLIGHT
		[TestMethod]
#else
		[Test]
#endif
		public void ShouldCreateSession()
		{
			var data = PortalClientTestHelper.Getclient(false, false).Session.Create().Synchronous(PortalClientTestHelper.CALL_TIMEOUT).ThrowFirstError().Result.Portal.Data;

			Assert.AreNotEqual(data.Count, 0, "No session data returned");
			Assert.AreNotEqual(new Guid(), data[0].SessionGUID);
		}
	}
}