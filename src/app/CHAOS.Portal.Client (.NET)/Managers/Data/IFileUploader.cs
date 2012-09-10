using System.ComponentModel;

namespace CHAOS.Portal.Client.Managers.Data
{
	public interface IFileUploader : INotifyPropertyChanged
	{
		double Progress { get; }

		TransactionState State { get; }

		void Start();
	}
}