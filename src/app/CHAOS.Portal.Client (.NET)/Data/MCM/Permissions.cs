using System.Collections.Generic;

namespace CHAOS.Portal.Client.Data.MCM
{
	public class Permissions<T>
	{
		public uint AccumulatedPermission { get; set; }
		public IList<Permission<T>> PermissionDetails { get; set; }
	}
}