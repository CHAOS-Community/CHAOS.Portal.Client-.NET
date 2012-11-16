#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;
#endif

using System.Linq;

namespace CHAOS.Portal.Client.Standard.Test.Extensions
{
#if SILVERLIGHT
	[TestClass, Tag("FormatType")]
#endif
	public class FormatTypeExtensionTest : APortalClientUnitTest
	{
#if SILVERLIGHT
		[TestMethod, Asynchronous, Tag("Get")]
#else
		[Test]
#endif
		public void ShouldGetFormatTypes()
		{
			TestData(
				CallPortal(c => c.FormatType.Get()),
				d =>
				{
					Assert.AreNotEqual(d.MCM.Data.Count, 0, "No FormatTypes returned");
					Assert.IsTrue(d.MCM.Data.All(t => t.Name != null), "Name not set on FormatType");
				});

			EndTest();
		}
	}
}