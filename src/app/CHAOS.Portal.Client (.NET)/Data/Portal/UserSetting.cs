using System;
using System.Xml.Linq;

namespace CHAOS.Portal.Client.Data.Portal
{
	public class UserSetting
	{
		public Guid GUID { get; set; }
		public Guid UserGUID { get; set; }
		public Guid ClientGUID { get; set; }
		public XDocument Settings { get; set; }
		public DateTime DateCreated { get; set; }
	}
}