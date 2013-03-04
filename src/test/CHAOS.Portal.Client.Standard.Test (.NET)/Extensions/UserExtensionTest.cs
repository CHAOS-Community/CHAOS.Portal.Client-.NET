#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;
#endif

using System;
using CHAOS.Portal.Client.Extensions;

namespace CHAOS.Portal.Client.Standard.Test.Extensions
{
#if SILVERLIGHT
	[TestClass, Tag("User")]
#endif
	public class UserExtensionTest : APortalClientUnitTest
	{
#if SILVERLIGHT
		[TestMethod, Asynchronous, Tag("Get")]
#else
		[Test]
#endif
		public void ShouldGetCurrentUser()
		{
			TestData(
				CallPortal(c => c.User().Get()),
					d =>
					{
						Assert.AreNotEqual(0, d.Count, "No user returned");
						Assert.AreNotEqual(new Guid(), d[0].Guid, "User Guid not set");
					});

			EndTest();
		}
	}
}