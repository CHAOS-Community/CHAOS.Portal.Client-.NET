#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;
#endif

using System;

namespace CHAOS.Portal.Client.Standard.Test.Extensions
{
#if SILVERLIGHT
	[TestClass]
#else
	[TestFixture]
#endif
	public class EmailPasswordExtensionTest
#if SILVERLIGHT
		: SilverlightTest
#endif
	{
#if SILVERLIGHT
		[TestMethod]
#else
		[Test]
#endif
		public void ShouldLogin()
		{
			var data = PortalClientTestHelper.Getclient(true, false).EmailPassword.Login(PortalClientTestHelper.LOGIN_EMAIL, PortalClientTestHelper.LOGIN_PASSWORD).Synchronous(PortalClientTestHelper.CALL_TIMEOUT).ThrowFirstError().Result.EmailPassword.Data;

			Assert.AreNotEqual(0, data.Count, "No user returned");
			Assert.AreNotEqual(new Guid(), data[0].GUID, "Returned Users GUID not set");
		}
	}
}