#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;
#endif

using CHAOS.Portal.Client.MCM.Extensions;

namespace CHAOS.Portal.Client.Standard.Test.Extensions
{
#if SILVERLIGHT
	[TestClass, Tag("Object")]
#endif
	public class ObjectExtensionTest : APortalClientUnitTest
	{
#if SILVERLIGHT
		[TestMethod, Asynchronous, Tag("Get")]
#else
		[Test]
#endif
		public void ShouldGetObjects()
		{
			TestData(
				CallPortal(c => c.Object().Get("", null, 0, 1, true, true, true, true)),
					d =>
					{
						Assert.AreNotEqual(d.Count, 0, "No objects returned");
						Assert.IsNotNull(d[0], "No objects returned");
						//Assert.AreNotEqual(d.MCM.TotalCount, 0, "TotalCount not set"); //TODO: Allow totalcount access in tests
					});

			EndTest();
		}
	}
}