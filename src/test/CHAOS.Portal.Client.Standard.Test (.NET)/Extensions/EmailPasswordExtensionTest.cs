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
	[TestClass, Tag("EmailPassword")]
#endif
	public class EmailPasswordExtensionTest : APortalClientUnitTest
	{
#if SILVERLIGHT
		[TestMethod, Asynchronous]
#else
		[Test]
#endif
		public void ShouldLogin()
		{
			TestData(
				CallPortal(c => c.EmailPassword.Login(PortalClientTestHelper.LOGIN_EMAIL, PortalClientTestHelper.LOGIN_PASSWORD), true, false),
				d =>
					{
						Assert.AreNotEqual(0, d.EmailPassword.Data.Count, "No user returned");
						Assert.AreNotEqual(new Guid(), d.EmailPassword.Data[0].GUID, "Returned Users GUID not set");
					});
			
			EndTest();
		}
	}
}
