using System;

namespace CHAOS.Portal.Client.Data
{
	public class AuthKey
	{
		public string Token { get; set; }
		public string Name { get; set; }
		public Guid UserGuid { get; set; }
	}
}