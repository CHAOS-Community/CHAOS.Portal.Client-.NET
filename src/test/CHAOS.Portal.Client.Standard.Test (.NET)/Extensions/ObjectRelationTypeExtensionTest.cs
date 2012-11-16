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
	[TestClass, Tag("ObjectRelationType")]
#endif
	public class ObjectRelationTypeExtensionTest : APortalClientUnitTest
	{
#if SILVERLIGHT
		[TestMethod, Asynchronous, Tag("Get")]
#else
		[Test]
#endif
		public void ShouldGetObjectRelationTypes()
		{
			TestData(
				CallPortal(c => c.ObjectRelationType.Get()),
					d =>
					{
						Assert.AreNotEqual(d.MCM.Data.Count, 0, "No ObjectRelationTypes returned");
						Assert.IsTrue(d.MCM.Data.All(t => t.Name != null), "Name not set on ObjectRelationType");
					});

			EndTest();
		}
	}
}