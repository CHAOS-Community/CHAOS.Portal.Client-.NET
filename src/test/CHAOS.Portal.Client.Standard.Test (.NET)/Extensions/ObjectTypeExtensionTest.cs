#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using System.Linq;
using NUnit.Framework;
#endif

namespace CHAOS.Portal.Client.Standard.Test.Extensions
{
#if SILVERLIGHT
	[TestClass]
#else
	[TestFixture]
#endif
	public class ObjectTypeExtensionTest
#if SILVERLIGHT
		: SilverlightTest
#endif
	{
#if SILVERLIGHT
		[TestMethod]
#else
		[Test]
#endif
		public void ShouldGetObjectTypes()
		{
			var data = PortalClientTestHelper.Getclient().ObjectType.Get().Synchronous(PortalClientTestHelper.CALL_TIMEOUT).ThrowFirstError().Result.MCM.Data;

			Assert.AreNotEqual(data.Count, 0, "No ObjectTypes returned");
			Assert.IsTrue(data.All(o => o.Name != null), "Name not set on ObjectType");
		}
	}
}