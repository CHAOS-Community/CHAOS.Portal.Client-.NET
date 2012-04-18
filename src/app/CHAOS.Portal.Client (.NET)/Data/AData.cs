using System.ComponentModel;

namespace CHAOS.Portal.Client.Data
{
	public abstract class AData : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		protected void RaisePropertyChanged(string propertyName)
		{
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}