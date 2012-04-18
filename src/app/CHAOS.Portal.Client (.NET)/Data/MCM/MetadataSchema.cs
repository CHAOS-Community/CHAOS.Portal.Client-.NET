using System;
using System.Xml.Linq;

namespace CHAOS.Portal.Client.Data.MCM
{
	public class MetadataSchema
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public XElement SchemaXML { get; set; }
		public DateTime DateCreated { get; set; }
	}
}