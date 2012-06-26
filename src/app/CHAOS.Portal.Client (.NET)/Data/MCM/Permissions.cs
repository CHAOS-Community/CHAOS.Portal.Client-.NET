using System.Collections.Generic;

namespace CHAOS.Portal.Client.Data.MCM
{
	public class FolderPermission
	{
		public uint AccumulatedPermission { get; set; }
		public IList<Permission> PermissionDetails { get; set; }
	}
}