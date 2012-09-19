using System;
using System.ComponentModel;

namespace CHAOS.Portal.Client.Managers
{
	public interface IFileUploader : INotifyPropertyChanged
	{
		event EventHandler Completed;

		TransactionState State { get; }
		double Progress { get; }
		Guid ObjectGUID { get; }
		uint FormatTypeID { get; }
		string FileName { get; }
		ulong FileSize { get; }

		void Start();
	}
}