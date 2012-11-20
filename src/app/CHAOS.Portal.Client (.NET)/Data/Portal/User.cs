using System;

namespace CHAOS.Portal.Client.Data.Portal
{
	public class User : AData
	{
		private Guid? _sessionID;
		public Guid? SessionID
		{
			get { return _sessionID; }
			set
			{
				_sessionID = value;
				RaisePropertyChanged("SessionGUID");
			}
		}

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

		private string _firstname;
		public string Firstname
		{
			get { return _firstname; }
			set
			{
				_firstname = value;
				RaisePropertyChanged("Firstname");
			}
		}

		private string _middlename;
		public string Middlename
		{
			get { return _middlename; }
			set
			{
				_middlename = value;
				RaisePropertyChanged("Middlename");
			}
		}

		private string _lastname;
		public string Lastname
		{
			get { return _lastname; }
			set
			{
				_lastname = value;
				RaisePropertyChanged("Lastname");
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