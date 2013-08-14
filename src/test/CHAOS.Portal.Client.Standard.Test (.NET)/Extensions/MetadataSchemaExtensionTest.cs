#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;
#endif

using System.Linq;
using CHAOS.Portal.Client.MCM.Extensions;

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
				CallPortalWithPagedResult(c => c.MetadataSchema().Get()),
					d =>
					{
						Assert.AreNotEqual(d.Count, 0, "No MetadataSchemas returned");
						Assert.IsTrue(d.All(s => s.Name != null && s.SchemaXML != null), "Name or SchemaXML not set on MetadataSchema");
					});

			EndTest();
		}
	}
}