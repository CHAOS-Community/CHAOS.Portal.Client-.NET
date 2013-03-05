using System;
using System.Xml.Linq;

namespace CHAOS.Portal.Client.Data
{
	public class ClientSettings
	{
		public Guid GUID { get; set; }
		public Guid ClientGUID { get; set; }
		public XElement Settings { get; set; }
		public DateTime DateCreated { get; set; }
	}
}