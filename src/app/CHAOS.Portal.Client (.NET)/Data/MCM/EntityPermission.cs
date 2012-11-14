using System;

namespace CHAOS.Portal.Client.Data.MCM
{
	public class EntityPermission<T>
	{
		public Guid GUID { get; set; }
		public T Permission { get; set; }
	}
}