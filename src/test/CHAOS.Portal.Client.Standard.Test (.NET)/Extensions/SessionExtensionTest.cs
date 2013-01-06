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
	[TestClass, Tag("Session")]
#else
	[TestFixture]
#endif
	public class SessionExtensionTest : APortalClientUnitTest
	{
#if SILVERLIGHT
		[TestMethod, Asynchronous, Tag("Create")]
#else
		[Test]
#endif
		public void ShouldCreateSession()
		{
			TestData(
				CallPortal(c => c.Session().Create(), false, false, false),
					d =>
					{
						Assert.AreNotEqual(d.Count, 0, "No session data returned");
						Assert.AreNotEqual(new Guid(), d[0].GUID);
					});

			EndTest();
		}
	}
}