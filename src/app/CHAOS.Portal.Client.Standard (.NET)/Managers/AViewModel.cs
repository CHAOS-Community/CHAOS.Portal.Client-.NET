using System.ComponentModel;

namespace CHAOS.Portal.Client.Standard.Managers
{
	public class AViewModel : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		protected void RaisePropertyChanged(string propertyName, bool runOnDispatcher = true)
		{
#if SILVERLIGHT
			if (!runOnDispatcher || System.Windows.Deployment.Current.Dispatcher.CheckAccess())
#endif
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
#if SILVERLIGHT
			else
				System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => PropertyChanged(this, new PropertyChangedEventArgs(propertyName)));
#endif
		}

		#endregion
	}
}