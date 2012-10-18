using System;
using CHAOS.Events;

namespace CHAOS.Portal.Client.Managers
{
	public interface ILoadingManager
	{
		event EventHandler<DataEventArgs<Exception>> ServiceFailed;
		event EventHandler Loaded; 
	}
}