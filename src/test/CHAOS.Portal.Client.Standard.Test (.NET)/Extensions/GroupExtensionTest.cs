#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;
#endif

using System;
using System.Linq;
using CHAOS.Portal.Client.Extensions;

namespace CHAOS.Portal.Client.Standard.Test.Extensions
{
#if SILVERLIGHT
	[TestClass, Tag("Group")]
#endif
	public class GroupExtensionTest : APortalClientUnitTest
	{
#if SILVERLIGHT
		[TestMethod, Asynchronous, Tag("Get")]
#else
		[Test]
#endif
		public void ShouldGetGroups()
		{
			TestData(
				CallPortal(c => c.Group().Get()),
					d =>
					{
						Assert.AreNotEqual(d.Count, 0, "No Groups returned");
						Assert.IsTrue(d.All(g => g.Name != null && g.GUID != new Guid()), "Name or GUID not set on Group");
					});

			EndTest();
		}
	}
}