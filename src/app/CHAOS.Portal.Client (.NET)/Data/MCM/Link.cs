using System;

namespace CHAOS.Portal.Client.Data.MCM
{
	public class Link
	{
		public uint FolderID { get; set; }
		public string ObjectGUID { get; set; }
		public uint ObjectFolderTypeID { get; set; }
		public DateTime DateCreated { get; set; }
	}
}