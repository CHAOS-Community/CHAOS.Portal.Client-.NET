using System;

namespace CHAOS.Portal.Client.Data.MCM
{
	public class ObjectRelation
	{
		public Guid Object1GUID { get; set; }

		public Guid Object2GUID { get; set; }

		public uint ObjectRelationTypeID { get; set; }

		public int? Sequence { get; set; }

		public DateTime DateCreated { get; set; }
	}
}