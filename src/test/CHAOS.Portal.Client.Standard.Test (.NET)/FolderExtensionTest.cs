#if SILVERLIGHT
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
#else
using NUnit.Framework;
#endif

namespace CHAOS.Portal.Client.Standard.Test
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
		[TestMethod, Asynchronous, Timeout(5000)]
#else
		[Test]
#endif
		public void ShouldGetFolderPermissons()
		{
			var client = PortalClientTest.Getclient(true, true);

			var folderGetState = client.Folder.Get(null, null, null).Synchronous(PortalClientTest.CALL_TIMEOUT);

			if (folderGetState.Error != null) throw folderGetState.Error;
			if (folderGetState.Result.MCM.Error != null) throw folderGetState.Result.MCM.Error;
			if (folderGetState.Result.MCM.Data.Count == 0) Assert.Fail("No folders to test");

			var state = client.Folder.GetPermission(folderGetState.Result.MCM.Data[0].ID).Synchronous(PortalClientTest.CALL_TIMEOUT);

			if (state.Error != null) throw state.Error;
			if (state.Result.MCM.Error != null) throw state.Result.MCM.Error;
			if (state.Result.MCM.Data.Count == 0) Assert.Fail("No permissions recieved");

			Assert.IsNotNull(state.Result.MCM.Data[0].UserPermissions);
			Assert.IsNotNull(state.Result.MCM.Data[0].GroupPermissions);
		}
	}
}