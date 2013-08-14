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
				CallPortalWithPagedResult(c => c.ObjectRelationType().Get()),
					d =>
					{
						Assert.AreNotEqual(d.Count, 0, "No ObjectRelationTypes returned");
						Assert.IsTrue(d.All(t => t.Name != null), "Name not set on ObjectRelationType");
					});

			EndTest();
		}
	}
}