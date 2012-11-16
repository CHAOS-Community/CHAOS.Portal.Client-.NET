#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;
#endif

namespace CHAOS.Portal.Client.Standard.Test.Extensions
{
#if SILVERLIGHT
	[TestClass]
#else
	[TestFixture]
#endif
	public class ObjectExtensionTest
#if SILVERLIGHT
		: SilverlightTest
#endif
	{
#if SILVERLIGHT
		[TestMethod]
#else
		[Test]
#endif
		public void ShouldGetObjects()
		{
			var mcmData = PortalClientTestHelper.GetClient().Object.Get("", null, 0, 1, true, true, true, true).Synchronous(PortalClientTestHelper.CALL_TIMEOUT).ThrowFirstError().Result.MCM;

			Assert.AreNotEqual(mcmData.Data.Count, 0, "No objects returned");
			Assert.IsNotNull(mcmData.Data[0], "No objects returned");
			Assert.AreNotEqual(mcmData.TotalCount, 0, "TotalCount not set");
		}
	}
}