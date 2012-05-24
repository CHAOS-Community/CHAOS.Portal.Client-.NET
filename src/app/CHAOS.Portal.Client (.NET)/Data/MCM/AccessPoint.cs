using System;

namespace CHAOS.Portal.Client.Data.MCM
{
	public class AccessPoint
	{
		public UUID AccessPointGUID { get; set; }
		public UUID ObjectGUID { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public DateTime? DateCreated { get; set; }
		public DateTime? DateModified { get; set; }
	}
}