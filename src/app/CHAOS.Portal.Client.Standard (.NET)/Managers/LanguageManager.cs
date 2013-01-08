using System;
using System.Collections.ObjectModel;
using CHAOS.Events;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.MCM.Extensions;
using CHAOS.Portal.Client.Managers;

namespace CHAOS.Portal.Client.Standard.Managers
{
	public class LanguageManager : ILanguageManager
	{
		public event EventHandler<DataEventArgs<Exception>> ServiceFailed = delegate { };
		public event EventHandler Loaded = delegate { };

		private readonly IPortalClient _client;

		private readonly ObservableCollection<Language> _languages;
		public ObservableCollection<Language> Languages { get { return _languages; } }

		public LanguageManager(IPortalClient client)
		{
			_client = client;
			_languages = new ObservableCollection<Language>();

			if (_client.HasSession)
				GetLanguages();
			else
				_client.SessionAcquired += ClientSessionAquired;
		}

		private void ClientSessionAquired(object sender, EventArgs e)
		{
			_client.SessionAcquired -= ClientSessionAquired;
			GetLanguages();
		}

		private void GetLanguages()
		{
			_client.Language().Get().Callback = ClientLanguageGetCompleted;
		}

		private void ClientLanguageGetCompleted(ServiceResponse<Language> response, object token)
		{
			if(response.Error != null)
			{
				ServiceFailed(this, new DataEventArgs<Exception>(response.Error));
				return;
			}

			foreach (var language in response.Result.Results)
				_languages.Add(language);

			Loaded(this, EventArgs.Empty);
		}
	}
}