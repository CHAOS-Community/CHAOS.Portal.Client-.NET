using System.Collections.Generic;

namespace CHAOS.Portal.Client.MCM.Data
{
	public class Permissions<T>
	{
		public IList<EntityPermission<T>> UserPermissions { get; set; }
		public IList<EntityPermission<T>> GroupPermissions { get; set; }
	}
}