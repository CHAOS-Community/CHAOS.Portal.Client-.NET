using System;
using System.Collections.ObjectModel;

namespace CHAOS.Portal.Client.Data.MCM
{
	public class Object : AData
	{
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

		private int _ObjectTypeID;
		public int ObjectTypeID
		{
			get { return _ObjectTypeID; }
			set
			{
				_ObjectTypeID = value;
				RaisePropertyChanged("ObjectTypeID");
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

		private ObservableCollection<Metadata> _Metadatas;
		public ObservableCollection<Metadata> Metadatas
		{
			get { return _Metadatas; }
			set
			{
				_Metadatas = value;
				RaisePropertyChanged("Metadatas");
			}
		}

		private ObservableCollection<File> _Files;
		public ObservableCollection<File> Files
		{
			get { return _Files; }
			set
			{
				_Files = value;
				RaisePropertyChanged("Files");
			}
		}

		private ObservableCollection<ObjectRelation> _ObjectRelations;
		public ObservableCollection<ObjectRelation> ObjectRelations
		{
			get { return _ObjectRelations; }
			set
			{
				_ObjectRelations = value;
				RaisePropertyChanged("ObjectRelations");
			}
		}
	}
}