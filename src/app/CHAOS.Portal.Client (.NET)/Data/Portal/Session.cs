using System;

namespace CHAOS.Portal.Client.Data.Portal
{
	public class Session
	{
		public Guid SessionGUID { get; set; }
		public Guid UserGUID { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
	}
}