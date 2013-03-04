using CHAOS.Portal.Client.Extensions;

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
				CallPortal(c => c.EmailPassword().Login(PortalClientTestHelper.LoginEmail, PortalClientTestHelper.LoginPassword), true, false),
				d =>
					{
						Assert.AreNotEqual(0, d.Count, "No user returned");
						Assert.AreNotEqual(new Guid(), d[0].Guid, "Returned Users GUID not set");
					});
			
			EndTest();
		}
	}
}
