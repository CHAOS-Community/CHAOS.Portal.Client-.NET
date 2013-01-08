using System;
using CHAOS.Portal.Client.Data;

namespace CHAOS.Portal.Client.MCM.Data
{
	public class Folder : AData
	{
		private uint _id;
		public uint ID
		{
			get { return _id; }
			set
			{
				_id = value;
				RaisePropertyChanged("ID");
			}
		}

		private uint? _parentID;
		public uint? ParentID
		{
			get { return _parentID; }
			set
			{
				_parentID = value;
				RaisePropertyChanged("ParentID");
			}
		}

		private int _folderTypeID;
		public int FolderTypeID
		{
			get { return _folderTypeID; }
			set
			{
				_folderTypeID = value;
				RaisePropertyChanged("FolderTypeID");
			}
		}

		private Guid _subscriptionGUID;
		public Guid SubscriptionGUID
		{
			get { return _subscriptionGUID; }
			set
			{
				_subscriptionGUID = value;
				RaisePropertyChanged("SubscriptionGUID");
			}
		}

		private string _name;
		public string Name
		{
			get { return _name; }
			set
			{
				_name = value;
				RaisePropertyChanged("Name");
			}
		}

		private int _numberOfSubFolders;
		public int NumberOfSubFolders
		{
			get { return _numberOfSubFolders; }
			set
			{
				_numberOfSubFolders = value;
				RaisePropertyChanged("NumberOfSubFolders");
			}
		}

		private int _numberOfObjects;
		public int NumberOfObjects
		{
			get { return _numberOfObjects; }
			set
			{
				_numberOfObjects = value;
				RaisePropertyChanged("NumberOfObjects");
			}
		}

		private DateTime _dateCreated;
		public DateTime DateCreated
		{
			get { return _dateCreated; }
			set
			{
				_dateCreated = value;
				RaisePropertyChanged("DateCreated");
			}
		}
	}
}