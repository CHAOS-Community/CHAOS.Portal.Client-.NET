using System;
using CHAOS.Events;
using CHAOS.Portal.Client.Data;

namespace CHAOS.Portal.Client.Extensions
{
	public interface ISessionChangingExtension
	{
		event EventHandler<DataEventArgs<Session>> SessionChanged;
	}
}