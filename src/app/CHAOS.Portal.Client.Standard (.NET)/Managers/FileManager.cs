using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.Managers;
using System.Linq;

namespace CHAOS.Portal.Client.Standard.Managers
{
	public class FileManager : IFileManager
	{
		private readonly IObjectManager _ObjectManager;

		public FileManager(IObjectManager objectManager)
		{
			_ObjectManager = objectManager;
		}

		public File GetFileByID(int id)
		{
			return _ObjectManager.GetObjectByFileID(id, true, false, false).Files.FirstOrDefault(f => f.ID == id);
		}
	}
}