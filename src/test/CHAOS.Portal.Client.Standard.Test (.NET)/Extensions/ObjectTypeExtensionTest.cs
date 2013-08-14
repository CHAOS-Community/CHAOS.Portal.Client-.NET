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
	[TestClass, Tag("ObjectType")]
#endif
	public class ObjectTypeExtensionTest : APortalClientUnitTest
	{
#if SILVERLIGHT
		[TestMethod, Asynchronous, Tag("Get")]
#else
		[Test]
#endif
		public void ShouldGetObjectTypes()
		{
			TestData(
				CallPortalWithPagedResult(c => c.ObjectType().Get()),
					d =>
					{
						Assert.AreNotEqual(d.Count, 0, "No ObjectTypes returned");
						Assert.IsTrue(d.All(o => o.Name != null), "Name not set on ObjectType");
					});

			EndTest();
		}
	}
}