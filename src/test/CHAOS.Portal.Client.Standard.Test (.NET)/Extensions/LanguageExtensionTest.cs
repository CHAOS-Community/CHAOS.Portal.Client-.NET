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
				CallPortalWithPagedResult(c => c.Language().Get()),
					d =>
					{
						Assert.AreNotEqual(d.Count, 0, "No Languages returned");
						Assert.IsTrue(d.All(g => g.Name != null && g.LanguageCode != null), "Name or LanguageCode not set on Language");
					});

			EndTest();
		}
	}
}