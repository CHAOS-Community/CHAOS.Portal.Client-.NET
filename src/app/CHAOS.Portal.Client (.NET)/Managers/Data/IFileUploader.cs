using System;
using System.ComponentModel;

namespace CHAOS.Portal.Client.Managers.Data
{
	public interface IFileUploader : INotifyPropertyChanged
	{
		event EventHandler Completed;
		
		double Progress { get; }

		TransactionState State { get; }

		void Start();
	}
}