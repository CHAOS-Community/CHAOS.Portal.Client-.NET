#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;
#endif

using System;
using System.Linq;

namespace CHAOS.Portal.Client.Standard.Test.Extensions
{
#if SILVERLIGHT
	[TestClass]
#else
	[TestFixture]
#endif
	public class LanguageExtensionTest
#if SILVERLIGHT
		: SilverlightTest
#endif
	{
#if SILVERLIGHT
		[TestMethod]
#else
		[Test]
#endif
		public void ShouldGetLanguages()
		{
			var data = PortalClientTestHelper.GetClient().Language.Get().Synchronous(PortalClientTestHelper.CALL_TIMEOUT).ThrowFirstError().Result.MCM.Data;

			Assert.AreNotEqual(data.Count, 0, "No Languages returned");
			Assert.IsTrue(data.All(t => t.Name != null && t.LanguageCode != null), "Name or LanguageCode not set on Language");
		}
	}
}