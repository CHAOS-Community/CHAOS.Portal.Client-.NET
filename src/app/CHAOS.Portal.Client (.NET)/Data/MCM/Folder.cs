using System;

namespace CHAOS.Portal.Client.Data.MCM
{
	public class Folder : AData
	{
		private int _ID;
		public int ID
		{
			get { return _ID; }
			set
			{
				_ID = value;
				RaisePropertyChanged("ID");
			}
		}

		private int? _ParentID;
		public int? ParentID
		{
			get { return _ParentID; }
			set
			{
				_ParentID = value;
				RaisePropertyChanged("ParentID");
			}
		}

		private int _FolderTypeID;
		public int FolderTypeID
		{
			get { return _FolderTypeID; }
			set
			{
				_FolderTypeID = value;
				RaisePropertyChanged("FolderTypeID");
			}
		}

		private Guid _SubscriptionGUID;
		public Guid SubscriptionGUID
		{
			get { return _SubscriptionGUID; }
			set
			{
				_SubscriptionGUID = value;
				RaisePropertyChanged("SubscriptionGUID");
			}
		}

		private string _Title;
		public string Title
		{
			get { return _Title; }
			set
			{
				_Title = value;
				RaisePropertyChanged("Title");
			}
		}

		private int _NumberOfSubFolders;
		public int NumberOfSubFolders
		{
			get { return _NumberOfSubFolders; }
			set
			{
				_NumberOfSubFolders = value;
				RaisePropertyChanged("NumberOfSubFolders");
			}
		}

		private int _NumberOfObjects;
		public int NumberOfObjects
		{
			get { return _NumberOfObjects; }
			set
			{
				_NumberOfObjects = value;
				RaisePropertyChanged("NumberOfObjects");
			}
		}

		private DateTime _DateCreated;
		public DateTime DateCreated
		{
			get { return _DateCreated; }
			set
			{
				_DateCreated = value;
				RaisePropertyChanged("DateCreated");
			}
		}
	}
}