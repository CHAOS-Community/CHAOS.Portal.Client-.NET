#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;
#endif

using System;
using CHAOS.Portal.Client.Extensions;

namespace CHAOS.Portal.Client.Standard.Test.Extensions
{
#if SILVERLIGHT
	[TestClass, Tag("View")]
#else
	[TestFixture]
#endif
	public class ViewExtensionTest : APortalClientUnitTest
	{
#if SILVERLIGHT
		[TestMethod, Asynchronous, Tag("Get")]
#else
		[Test]
#endif
		public void ShouldGetSearchView()
		{
			TestData(
				CallPortal(c => c.View().Get<SearchView>("Search", null, null, null, 0, 3), true, false),
					d =>
					{
						Assert.AreNotEqual(0, d.Count, "No views returned");
						Assert.AreNotEqual(new Guid(), d[0].Guid, "User Guid not set");
					});

			EndTest();
		}
	}

	internal class SearchView
	{
		public Guid Guid { get; set; }
		public string Title { get; set; }
	}
}