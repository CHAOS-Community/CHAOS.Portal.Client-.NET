using System;
using System.Xml.Linq;

namespace CHAOS.Portal.Client.Data.Portal
{
	public class ClientSettings
	{
		public Guid GUID { get; set; }
		public Guid ClientGUID { get; set; }
		public XDocument Settings { get; set; }
		public DateTime DateCreated { get; set; }
	}
}