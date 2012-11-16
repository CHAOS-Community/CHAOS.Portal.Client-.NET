#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;
#endif

using System;
using System.Linq;

namespace CHAOS.Portal.Client.Standard.Test.Extensions
{
#if SILVERLIGHT
	[TestClass]
#else
	[TestFixture]
#endif
	public class GroupExtensionTest
#if SILVERLIGHT
		: SilverlightTest
#endif
	{
#if SILVERLIGHT
		[TestMethod]
#else
		[Test]
#endif
		public void ShouldGetGroups()
		{
			var data = PortalClientTestHelper.GetClient().Group.Get().Synchronous(PortalClientTestHelper.CALL_TIMEOUT).ThrowFirstError().Result.Portal.Data;

			Assert.AreNotEqual(data.Count, 0, "No Groups returned");
			Assert.IsTrue(data.All(g => g.Name != null && g.GUID != new Guid()), "Name or GUID not set on Group");
		}
	}
}