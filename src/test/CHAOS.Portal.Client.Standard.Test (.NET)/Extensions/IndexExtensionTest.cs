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
	[TestClass, Tag("Index")]
#endif
	public class IndexExtensionTest : APortalClientUnitTest
	{
#if SILVERLIGHT
		[TestMethod, Asynchronous, Tag("Get")]
#else
		[Test]
#endif
		public void ShouldDoFacetSearch()
		{
			TestData(
				CallPortal(c => c.Index.Search(null, "field:ObjectTypeID", null, 0, 1)),
					d =>
					{
						Assert.AreNotEqual(d.Index.Data.Count, 0, "No facets returned");
						Assert.IsNotNull(d.Index.Data[0], "No facets returned");
					});

			EndTest();
		}
	}
}