using System;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Input;
using CHAOS.Portal.Client.Standard.Managers;
using CHAOS.Portal.Client.Standard.Test;
using CHAOS.UI.MVVM.Commands;
using Microsoft.Silverlight.Testing;

namespace CHAOS.Portal.Client.Test
{
	public class MainPageViewModel : AViewModel
	{
		public ICommand RunTestsCommand { get; private set; }
		public UIElement Tests { get; private set; }
		public string ServicePath { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public bool UseLatest { get; set; }

		public MainPageViewModel()
		{
			RunTestsCommand = new DelegateCommand((Action)AddTests);

			var settings = IsolatedStorageSettings.ApplicationSettings;

			if(settings.Contains("ServicePath"))
				ServicePath = (string)settings["ServicePath"];
			if (settings.Contains("LoginEmail"))
				Email = (string)settings["LoginEmail"];
			if (settings.Contains("LoginPassword"))
				Password = (string)settings["LoginPassword"];
			if (settings.Contains("UseLatest"))
				UseLatest = (bool)settings["UseLatest"];
		}

		 private void AddTests()
		 {
			 PortalClientTestHelper.ServicePath = ServicePath;
			 PortalClientTestHelper.LoginEmail = Email;
			 PortalClientTestHelper.LoginPassword = Password;
			 PortalClientTestHelper.UseLatest = UseLatest;

			 var settings = IsolatedStorageSettings.ApplicationSettings;
			 settings["ServicePath"] = ServicePath;
			 settings["LoginEmail"] = Email;
			 settings["LoginPassword"] = Password;
			 settings["UseLatest"] = UseLatest;
			 settings.Save();

			 Tests = UnitTestSystem.CreateTestPage();
			 RaisePropertyChanged("Tests");
		 }
	}
}