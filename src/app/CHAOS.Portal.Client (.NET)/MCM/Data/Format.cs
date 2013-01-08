using System.Xml.Linq;

namespace CHAOS.Portal.Client.MCM.Data
{
	public class Format
	{
		public uint ID { get; set; }
		public uint FormatCategoryID { get; set; }
		public string Name { get; set; }
		public XDocument FormatXML { get; set; }
		public string MimeType { get; set; }
		public string Extension { get; set; }
	}
}