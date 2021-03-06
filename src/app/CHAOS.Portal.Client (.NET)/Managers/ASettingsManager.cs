using System;
using System.Collections.Generic;
using System.ComponentModel;
using CHAOS.Events;
using CHAOS.Tasks;

namespace CHAOS.Portal.Client.Managers
{
	public abstract class ASettingsManager<TSettings> : INotifyPropertyChanged where TSettings : class
	{
		public event EventHandler<DataEventArgs<Exception>> ManagerFailure = delegate { };
		
		private readonly IPortalClient _portalClient;
		
		private readonly RepeatTaskConcater _updateGuard;

		private readonly IDictionary<string, object> _settingValues;
		private readonly IDictionary<string, object> _settingDefaultValues;
		
		private bool? _serviceHadSettings;
		private bool _shouldSaveAfterGet;
		private bool _isApplyingSettings;

		protected IPortalClient PortalClient { get { return _portalClient; } }
		
		protected ASettingsManager(IPortalClient portalClient)
		{
			_portalClient = portalClient;
			_updateGuard = new RepeatTaskConcater(SetSettings) { WaitTime = 5000 };
			_settingValues = new Dictionary<string, object>();
			_settingDefaultValues = new Dictionary<string, object>();

			GetSettingsWhenReady();
		}

		#region Set / Get Settings

		protected void SetSetting<T>(string name, T value)
		{
			_settingValues[name] = value;

			if (!_isApplyingSettings)
				_updateGuard.Execute();
			
			RaisePropertyChanged(name);
		}

		protected T GetSetting<T>(string name)
		{
			return _settingValues.ContainsKey(name) ? (T)_settingValues[name] : _settingDefaultValues.ContainsKey(name) ? (T) _settingDefaultValues[name] : default(T);
		}

		protected void SetDefaultSetting<T>(string name, T value)
		{
			_settingDefaultValues[name] = value;
		}

		#endregion
		#region Service

		protected void ReportError(Exception error)
		{
			ManagerFailure(this, new DataEventArgs<Exception>(error));
		}

		#region Get

		protected abstract void GetSettingsFromService();
		protected abstract void ApplySettings(TSettings settings);
		
		protected virtual void NoSettingsExist()
		{
			
		}

		private void GetSettingsWhenReady()
		{
			if (!PortalClient.HasSession)
			{
				PortalClient.SessionAcquired += PortalClientSessionAcquired;
				return;
			}
			if (!PortalClient.HasClientGUID)
			{
				PortalClient.ClientGUIDSet += PortalClientClientGUIDSet;
				return;
			}

			GetSettingsFromService();
		}

		private void PortalClientSessionAcquired(object sender, EventArgs e)
		{
			PortalClient.SessionAcquired -= PortalClientSessionAcquired;
			GetSettingsWhenReady();
		}

		private void PortalClientClientGUIDSet(object sender, EventArgs e)
		{
			PortalClient.ClientGUIDSet -= PortalClientClientGUIDSet;
			GetSettingsWhenReady();
		}

		protected void SettingsRecievedFromService(IList<TSettings> settings)
		{
			if (settings == null || settings.Count == 0)
			{
				_serviceHadSettings = false;
				NoSettingsExist();
			}
			else
			{
				_serviceHadSettings = true;

				_isApplyingSettings = true;

				try
				{
					ApplySettings(settings[0]);
				}
				catch(Exception error)
				{
					ReportError(error);
				}

				_isApplyingSettings = false;
			}

			if (_shouldSaveAfterGet)
				_updateGuard.Execute();
		}

		#endregion
		#region Update

		protected abstract void SendSettingsToService(bool settingsExistOnService);

		private void SetSettings()
		{
			if (_serviceHadSettings.HasValue)
				SendSettingsToService(_serviceHadSettings.Value);
			else
				_shouldSaveAfterGet = true;
		}

		#endregion
		#endregion
		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		private void RaisePropertyChanged(string propertyName)
		{
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}