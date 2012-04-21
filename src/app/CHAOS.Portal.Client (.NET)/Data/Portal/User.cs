using System;

namespace CHAOS.Portal.Client.Data.Portal
{
	//CHAOS.Portal.Data.UserInfo
	public class User : AData
	{
		private int _ID;
		public int ID
		{
			get { return _ID; }
			set
			{
				_ID = value;
				RaisePropertyChanged("GUID");
			}
		}

		//TODO: Remove this
		private Guid _SessionID;
		public Guid SessionID
		{
			get { return _SessionID; }
			set
			{
				_SessionID = value;
				RaisePropertyChanged("SessionGUID");
			}
		}

		private Guid _GUID;
		public Guid GUID
		{
			get { return _GUID; }
			set
			{
				_GUID = value;
				RaisePropertyChanged("GUID");
			}
		}

		private string _Firstname;
		public string Firstname
		{
			get { return _Firstname; }
			set
			{
				_Firstname = value;
				RaisePropertyChanged("Firstname");
			}
		}

		//TODO: Rename to FirstName
		private string _Middlename;
		public string Middlename
		{
			get { return _Middlename; }
			set
			{
				_Middlename = value;
				RaisePropertyChanged("Middlename");
			}
		}

		//TODO: Rename to MiddleName
		private string _Lastname;
		public string Lastname
		{
			get { return _Lastname; }
			set
			{
				_Lastname = value;
				RaisePropertyChanged("Lastname");
			}
		}

		//TODO: Rename to LastName
		private string _Email;
		public string Email
		{
			get { return _Email; }
			set
			{
				_Email = value;
				RaisePropertyChanged("Email");
			}
		}

		private int _SystemPermission;
		public int SystemPermission
		{
			get { return _SystemPermission; }
			set
			{
				_SystemPermission = value;
				RaisePropertyChanged("SystemPermission");
			}
		}
	}
}