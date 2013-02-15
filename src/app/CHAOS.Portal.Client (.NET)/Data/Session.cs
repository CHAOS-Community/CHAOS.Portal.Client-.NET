using System;

namespace CHAOS.Portal.Client.Data
{
	public class Session
	{
		public Guid Guid { get; set; }
		public Guid UserGuid { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
	}
}