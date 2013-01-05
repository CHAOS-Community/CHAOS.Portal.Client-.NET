using System;
using System.Collections.ObjectModel;
using CHAOS.Events;
using CHAOS.Portal.Client.MCM.Data;

namespace CHAOS.Portal.Client.Managers
{
	public interface ILanguageManager : ILoadingManager
	{
		ObservableCollection<Language> Languages { get; }
	}
}