using System;
using System.Collections.ObjectModel;
using CHAOS.Portal.Client.Data;

namespace CHAOS.Portal.Client.MCM.Data
{
	public class Object : AData
	{
		private Guid _guid;
		public Guid GUID
		{
			get { return _guid; }
			set
			{
				_guid = value;
				RaisePropertyChanged("GUID");
			}
		}

		private uint _objectTypeID;
		public uint ObjectTypeID
		{
			get { return _objectTypeID; }
			set
			{
				_objectTypeID = value;
				RaisePropertyChanged("ObjectTypeID");
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

		private ObservableCollection<Metadata> _metadatas;
		public ObservableCollection<Metadata> Metadatas
		{
			get { return _metadatas; }
			set
			{
				_metadatas = value;
				RaisePropertyChanged("Metadatas");
			}
		}

		private ObservableCollection<File> _files;
		public ObservableCollection<File> Files
		{
			get { return _files; }
			set
			{
				_files = value;
				RaisePropertyChanged("Files");
			}
		}

		private ObservableCollection<ObjectRelation> _objectRelations;
		public ObservableCollection<ObjectRelation> ObjectRelations
		{
			get { return _objectRelations; }
			set
			{
				_objectRelations = value;
				RaisePropertyChanged("ObjectRelations");
			}
		}

		private ObservableCollection<AccessPoint> _accessPoints;
		public ObservableCollection<AccessPoint> AccessPoints
		{
			get { return _accessPoints; }
			set
			{
				_accessPoints = value;
				RaisePropertyChanged("AccessPoints");
			}
		}
	}
}