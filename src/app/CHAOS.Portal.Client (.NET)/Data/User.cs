using System;

namespace CHAOS.Portal.Client.Data
{
	public class User : AData
	{
		private Guid? _guid;
		public Guid? GUID
		{
			get { return _guid; }
			set
			{
				_guid = value;
				RaisePropertyChanged("GUID");
			}
		}

		private string _email;
		public string Email
		{
			get { return _email; }
			set
			{
				_email = value;
				RaisePropertyChanged("Email");
			}
		}

		private int _systemPermission;
		public int SystemPermission
		{
			get { return _systemPermission; }
			set
			{
				_systemPermission = value;
				RaisePropertyChanged("SystemPermission");
			}
		}
	}
}