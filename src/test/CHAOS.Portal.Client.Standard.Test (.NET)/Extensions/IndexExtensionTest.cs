using NUnit.Framework;
#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else

#endif

namespace CHAOS.Portal.Client.Standard.Test.Extensions
{
#if SILVERLIGHT
	[TestClass]
#else
	[TestFixture]
#endif
	public class IndexExtensionTest
#if SILVERLIGHT
		: SilverlightTest
#endif
	{
#if SILVERLIGHT
		[TestMethod]
#else
		[Test]
#endif
		public void ShouldDoFacetSearch()
		{
			var indexData = PortalClientTestHelper.Getclient().Index.Search(null, "field:ObjectTypeID", null, 0, 1).Synchronous(PortalClientTestHelper.CALL_TIMEOUT).ThrowFirstError().Result.Index;

			Assert.AreNotEqual(indexData.Data.Count, 0, "No facets returned");
			Assert.IsNotNull(indexData.Data[0], "No facets returned");
		}
	}
}