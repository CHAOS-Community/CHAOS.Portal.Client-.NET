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
				CallPortal(c => c.User.Get()),
					d =>
					{
						Assert.AreNotEqual(0, d.Portal.Data.Count, "No user returned");
						Assert.AreNotEqual(new Guid(), d.Portal.Data[0].GUID, "User Guid not set");
					});

			EndTest();
		}
	}
}