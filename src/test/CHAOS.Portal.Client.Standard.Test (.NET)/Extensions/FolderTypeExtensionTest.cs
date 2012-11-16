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
	[TestClass, Tag("FolderType")]
#endif
	public class FolderTypeExtensionTest : APortalClientUnitTest
	{
#if SILVERLIGHT
		[TestMethod, Asynchronous, Tag("Get")]
#else
		[Test]
#endif
		public void ShouldGetFolderTypes()
		{
			TestData(
				CallPortal(c => c.FolderType.Get()),
				d =>
				{
					Assert.AreNotEqual(d.MCM.Data.Count, 0, "No FolderTypes returned");
					Assert.IsTrue(d.MCM.Data.All(t => t.Name != null), "Name not set on FolderType");
				});

			EndTest();
		}
	}
}