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
	[TestClass, Tag("MetadataSchema")]
#endif
	public class MetadataSchemaExtensionTest : APortalClientUnitTest
	{
#if SILVERLIGHT
		[TestMethod, Asynchronous, Tag("Get")]
#else
		[Test]
#endif
		public void ShouldGetMetadataSchemas()
		{
			TestData(
				CallPortal(c => c.MetadataSchema.Get()),
					d =>
					{
						Assert.AreNotEqual(d.MCM.Data.Count, 0, "No MetadataSchemas returned");
						Assert.IsTrue(d.MCM.Data.All(s => s.Name != null && s.SchemaXML != null), "Name or SchemaXML not set on MetadataSchema");
					});

			EndTest();
		}
	}
}