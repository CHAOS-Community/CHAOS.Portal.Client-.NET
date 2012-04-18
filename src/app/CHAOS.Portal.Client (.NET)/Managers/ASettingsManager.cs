using System;
using System.Collections.Generic;
using System.ComponentModel;
using CHAOS.Common.Events;
using CHAOS.Common.Utilities;

namespace CHAOS.Portal.Client.Managers
{
	
	public abstract class ASettingsManager<TSettings> : INotifyPropertyChanged where TSettings : class
	{
		public event EventHandler<DataEventArgs<Exception>> ManagerFailure = delegate { };
		
		private readonly IPortalClient _PortalClient;
		
		private readonly RepeatActionConcater _UpdateGuard;

		private readonly IDictionary<string, object> _SettingValues;
		private readonly IDictionary<string, object> _SettingDefaultValues;
		
		private readonly HashSet<string> _ChangedProperties;
		private bool? _ServiceHadSettings;
		private bool _ShouldSaveAfterGet;
		private bool _LockSettings;

		public IPortalClient PortalClient { get { return _PortalClient; } }
		
		protected ASettingsManager(IPortalClient portalClient)
		{
			_PortalClient = portalClient;
			_UpdateGuard = new RepeatActionConcater(SetSettings) {WaitTime = 5000};
			_SettingValues = new Dictionary<string, object>();
			_SettingDefaultValues = new Dictionary<string, object>();
			_ChangedProperties = new HashSet<string>();

			GetSettingsWhenReady();
		}

		#region Set / Get Settings

		protected void SetSetting<T>(string name, T value)
		{
			if (_LockSettings && _ChangedProperties.Contains(name)) return;
			if (!_ChangedProperties.Contains(name)) _ChangedProperties.Add(name);
			_SettingValues[name] = value;
			_UpdateGuard.Execute();
			RaisePropertyChanged(name);
		}

		protected T GetSetting<T>(string name)
		{
			return _SettingValues.ContainsKey(name) ? (T)_SettingValues[name] : _SettingDefaultValues.ContainsKey(name) ? (T) _SettingDefaultValues[name] : default(T);
		}

		protected void SetDefaultSetting<T>(string name, T value)
		{
			_SettingDefaultValues[name] = value;
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
			if (settings.Count == 0)
			{
				_ServiceHadSettings = false;
			}
			else
			{
				_ServiceHadSettings = true;

				_LockSettings = true;

				ApplySettings(settings[0]);

				_LockSettings = false;
			}

			_ChangedProperties.Clear();

			if (_ShouldSaveAfterGet)
				_UpdateGuard.Execute();
		}

		#endregion
		#region Update

		protected abstract void SendSettingsToService(bool settingsExistOnService);

		private void SetSettings()
		{
			if (_ServiceHadSettings.HasValue)
				SendSettingsToService(_ServiceHadSettings.Value);
			else
				_ShouldSaveAfterGet = true;
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