using System;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IClientGUIDDependentExtension
	{
		Guid ClientGUID { set; } 
	}
}