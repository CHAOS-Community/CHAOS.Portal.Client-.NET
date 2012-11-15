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
	public class FormatTypeExtensionTest
#if SILVERLIGHT
		: SilverlightTest
#endif
	{
#if SILVERLIGHT
		[TestMethod]
#else
		[Test]
#endif
		public void ShouldGetFormatTypes()
		{
			var data = PortalClientTestHelper.Getclient().FormatType.Get().Synchronous(PortalClientTestHelper.CALL_TIMEOUT).ThrowFirstError().Result.MCM.Data;

			Assert.AreNotEqual(data.Count, 0, "No FormatTypes returned");
			Assert.IsTrue(data.All(t => t.Name != null), "Name not set on FormatType");
		}
	}
}