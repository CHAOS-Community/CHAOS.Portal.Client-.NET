using System;

namespace CHAOS.Portal.Client.MCM.Data
{
	public class EntityPermission<T>
	{
		public Guid GUID { get; set; }
		public T Permission { get; set; }
	}
}