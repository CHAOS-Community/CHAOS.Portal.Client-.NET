using System;

namespace CHAOS.Portal.Client.Data
{
	public class Session
	{
		public Guid GUID { get; set; }
		public Guid UserGUID { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
	}
}