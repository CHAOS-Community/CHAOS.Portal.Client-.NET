using System;
using System.Xml.Linq;

namespace CHAOS.Portal.Client.MCM.Data
{
	public class UserProfile
	{
		public Guid MetadataSchemaGuid { get; set; }
		public Guid EditingUserGuid { get; set; }
		public XDocument MetadataXml { get; set; }
		public DateTime DateCreated { get; set; }
	}
}