#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;
#endif

using CHAOS.Portal.Client.Data.MCM;

namespace CHAOS.Portal.Client.Standard.Test.Extensions
{
#if SILVERLIGHT
	[TestClass, Tag("Folder")]
#else
	[TestFixture]
#endif
	public class FolderExtensionTest : APortalClientUnitTest
	{
#if SILVERLIGHT
		[TestMethod, Asynchronous, Tag("Get")]
#else
		[Test]
#endif
		public void ShouldGetFolderPermissons()
		{
			Folder folder = null;

			TestData(
				CallPortal(c => c.Folder.Get()),
					d =>
					{
						Assert.AreNotEqual(0, d.MCM.Data.Count, "No folders to test");
						folder = d.MCM.Data[0];
					});

			TestData(
				CallPortal(c => c.Folder.GetPermission(folder.ID)),
					d =>
					{
						Assert.AreNotEqual(0, d.MCM.Data.Count, "No permissions recieved");
						Assert.IsNotNull(d.MCM.Data[0].UserPermissions);
						Assert.IsNotNull(d.MCM.Data[0].GroupPermissions);
					});

			EndTest();
		}
	}
}