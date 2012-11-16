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
	[TestClass, Tag("Language")]
#endif
	public class LanguageExtensionTest : APortalClientUnitTest
	{
#if SILVERLIGHT
		[TestMethod, Asynchronous, Tag("Get")]
#else
		[Test]
#endif
		public void ShouldGetLanguages()
		{
			TestData(
				CallPortal(c => c.Language.Get()),
					d =>
					{
						Assert.AreNotEqual(d.MCM.Data.Count, 0, "No Languages returned");
						Assert.IsTrue(d.MCM.Data.All(g => g.Name != null && g.LanguageCode != null), "Name or LanguageCode not set on Language");
					});

			EndTest();
		}
	}
}