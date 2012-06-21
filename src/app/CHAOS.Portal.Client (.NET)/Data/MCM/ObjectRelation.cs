using System;

namespace CHAOS.Portal.Client.Data.MCM
{
	public class ObjectRelation
	{
		public ObjectRelation(Guid object1GUID, Guid object2GUID, uint objectRelationTypeID, int? sequence = null, DateTime? dateCreated = null)
		{
			Object1GUID = object1GUID;
			Object2GUID = object2GUID;
			ObjectRelationTypeID = objectRelationTypeID;
			Sequence = sequence;
			DateCreated = dateCreated.HasValue ? dateCreated.Value : DateTime.Now;
		}

		public Guid Object1GUID { get; set; }

		public Guid Object2GUID { get; set; }

		public uint ObjectRelationTypeID { get; set; }

		public int? Sequence { get; set; }

		public DateTime DateCreated { get; set; }
	}
}