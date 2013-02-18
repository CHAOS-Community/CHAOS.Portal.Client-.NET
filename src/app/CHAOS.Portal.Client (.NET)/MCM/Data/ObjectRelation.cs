using System;

namespace CHAOS.Portal.Client.MCM.Data
{
	public class ObjectRelation
	{
		public ObjectRelation()
		{
			
		}
		
		public ObjectRelation(Guid object1Guid, Guid object2Guid, uint objectRelationTypeID, int? sequence = null, DateTime? dateCreated = null)
		{
			Object1Guid = object1Guid;
			Object2Guid = object2Guid;
			ObjectRelationTypeID = objectRelationTypeID;
			Sequence = sequence;
			DateCreated = dateCreated.HasValue ? dateCreated.Value : DateTime.Now;
		}

		public Guid Object1Guid { get; set; }

		public Guid Object2Guid { get; set; }

		public uint ObjectRelationTypeID { get; set; }

		public int? Sequence { get; set; }

		public DateTime DateCreated { get; set; }
	}
}