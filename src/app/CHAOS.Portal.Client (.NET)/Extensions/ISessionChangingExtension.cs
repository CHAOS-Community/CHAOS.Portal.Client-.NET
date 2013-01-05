using System;
using CHAOS.Portal.Client.Data.Portal;

namespace CHAOS.Portal.Client.Extensions
{
	public interface ISessionChangingExtension
	{
		event EventHandler SessionChanged;
		Session Session { get; set; }
	}
}