using System;
using System.Xml.Linq;

namespace CHAOS.Portal.Client.Data.MCM
{
	public class MetadataSchema
	{
		public Guid GUID { get; set; }
		public string Name { get; set; }
		public XElement SchemaXML { get; set; }
		public DateTime DateCreated { get; set; }
	}
}