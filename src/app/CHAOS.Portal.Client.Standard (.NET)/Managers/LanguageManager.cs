using System;
using System.Collections.ObjectModel;
using CHAOS.Events;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.Managers;
using CHAOS.Extensions;

namespace CHAOS.Portal.Client.Standard.Managers
{
	public class LanguageManager : ILanguageManager
	{
		public event EventHandler<DataEventArgs<Exception>> ServiceFailed = delegate { };

		private readonly IPortalClient _Client;

		private readonly ObservableCollection<Language> _Languages;
		public ObservableCollection<Language> Languages { get { return _Languages; } }

		public LanguageManager(IPortalClient client)
		{
			_Client = client;
			_Languages = new ObservableCollection<Language>();

			if (_Client.HasSession)
				GetLanguages();
			else
				_Client.SessionAcquired += ClientSessionAquired;
		}

		private void ClientSessionAquired(object sender, EventArgs e)
		{
			_Client.SessionAcquired -= ClientSessionAquired;
			GetLanguages();
		}

		private void GetLanguages()
		{
			_Client.Language.Get(null, null).Callback = ClientLanguageGetCompleted;
		}

		private void ClientLanguageGetCompleted(IServiceResult_MCM<Language> result, Exception error, object token)
		{
			if(!error.IsNull())
			{
				ServiceFailed(this, new DataEventArgs<Exception>(error));
				return;
			}

			foreach (var language in result.MCM.Data)
				_Languages.Add(language);
		}
	}
}