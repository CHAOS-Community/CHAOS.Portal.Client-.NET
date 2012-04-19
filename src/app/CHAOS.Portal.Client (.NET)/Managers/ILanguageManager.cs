using System;
using System.Collections.ObjectModel;
using CHAOS.Events;
using CHAOS.Portal.Client.Data.MCM;

namespace CHAOS.Portal.Client.Managers
{
	public interface ILanguageManager
	{
		event EventHandler<DataEventArgs<Exception>> ServiceFailed;

		ObservableCollection<Language> Languages { get; }
	}
}