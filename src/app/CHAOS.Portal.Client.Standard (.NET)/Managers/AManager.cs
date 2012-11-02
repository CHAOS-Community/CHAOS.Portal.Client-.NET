using System;
#if SILVERLIGHT
using System.Windows;
#endif

namespace CHAOS.Portal.Client.Standard.Managers
{
	public abstract class AManager
	{
		 protected void UpdatePublicProperty(Action action)
		 {
#if SILVERLIGHT
			if (Deployment.Current.Dispatcher.CheckAccess())
				action();
			else
				Deployment.Current.Dispatcher.BeginInvoke(action);
#else
			 action(); //TODO: Find alternative to dispatcher
#endif
		 }
	}
}