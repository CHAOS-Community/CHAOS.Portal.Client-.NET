using System;

namespace CHAOS.Portal.Client.Data.Portal
{
	//CHAOS.Portal.Data.Session
	public class Session
	{
		public Guid SessionID { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
	}
}