using System;
using System.Xml.Linq;

namespace CHAOS.Portal.Client.MCM.Data
{
	public class MetadataSchema
	{
		public Guid Guid { get; set; }
		public string Name { get; set; }
		public XElement SchemaXML { get; set; }
		public DateTime DateCreated { get; set; }
	}
}