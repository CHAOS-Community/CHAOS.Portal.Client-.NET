using System;
using CHAOS.Portal.Client.Data;

namespace CHAOS.Portal.Client.MCM.Data
{
	public class ObjectFolder : AData
	{
		public uint ID { get; set; }
		public uint? ParentID { get; set; }
		public uint FolderTypeID { get; set; }
		public uint ObjectFolderTypeID { get; set; }
		public Guid? SubscriptionGuid { get; set; }
		public string Name { get; set; }
		public DateTime DateCreated { get; set; }
	}
}