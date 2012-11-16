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
				CallPortal(c => c.ObjectType.Get()),
					d =>
					{
						Assert.AreNotEqual(d.MCM.Data.Count, 0, "No ObjectTypes returned");
						Assert.IsTrue(d.MCM.Data.All(o => o.Name != null), "Name not set on ObjectType");
					});

			EndTest();
		}
	}
}