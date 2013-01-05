using System;

namespace CHAOS.Portal.Client.MCM.Data
{
	public class Link
	{
		public uint FolderID { get; set; }
		public string ObjectGUID { get; set; }
		public uint ObjectFolderTypeID { get; set; }
		public DateTime DateCreated { get; set; }
	}
}