#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;

#endif

namespace CHAOS.Portal.Client.Standard.Test.Extensions
{
#if SILVERLIGHT
	[TestClass]
#else
	[TestFixture]
#endif
	public class FolderExtensionTest
#if SILVERLIGHT
		: SilverlightTest
#endif
	{
#if SILVERLIGHT
		[TestMethod]
#else
		[Test]
#endif
		public void ShouldGetFolderPermissons()
		{
			var client = PortalClientTestHelper.Getclient();

			var folderGetState = client.Folder.Get().Synchronous(PortalClientTestHelper.CALL_TIMEOUT).ThrowFirstError();
			if (folderGetState.Result.MCM.Data.Count == 0) Assert.Fail("No folders to test");

			var state = client.Folder.GetPermission(folderGetState.Result.MCM.Data[0].ID).Synchronous(PortalClientTestHelper.CALL_TIMEOUT).ThrowFirstError();
			if (state.Result.MCM.Data.Count == 0) Assert.Fail("No permissions recieved");

			Assert.IsNotNull(state.Result.MCM.Data[0].UserPermissions);
			Assert.IsNotNull(state.Result.MCM.Data[0].GroupPermissions);
		}
	}
}