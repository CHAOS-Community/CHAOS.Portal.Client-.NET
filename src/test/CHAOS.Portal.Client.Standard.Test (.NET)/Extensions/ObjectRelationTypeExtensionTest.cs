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
	public class ObjectRelationTypeExtensionTest
#if SILVERLIGHT
		: SilverlightTest
#endif
	{
#if SILVERLIGHT
		[TestMethod]
#else
		[Test]
#endif
		public void ShouldGetObjectRelationTypes()
		{
			var data = PortalClientTestHelper.GetClient().ObjectRelationType.Get().Synchronous(PortalClientTestHelper.CALL_TIMEOUT).ThrowFirstError().Result.MCM.Data;

			Assert.AreNotEqual(data.Count, 0, "No ObjectRelationTypes returned");
			Assert.IsTrue(data.All(t => t.Name != null), "Name not set on ObjectRelationType");
		}
	}
}