using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;

namespace CHAOS.Portal.Client.MCM.Data
{
	public class Metadata : AData
	{
		private string _languageCode;
		public string LanguageCode
		{
			get { return _languageCode; }
			set
			{
				_languageCode = value;
				RaisePropertyChanged("LanguageCode");
			}
		}

		private Guid _metadataSchemaGUID;
		public Guid MetadataSchemaGUID
		{
			get { return _metadataSchemaGUID; }
			set
			{
				_metadataSchemaGUID = value;
				RaisePropertyChanged("MetadataSchemaGUID");
			}
		}
			
		private uint? _revisionID;
		public uint? RevisionID
		{
			get { return _revisionID; }
			set
			{
				_revisionID = value;
				RaisePropertyChanged("RevisionID");
			}
		}

		private Guid _editingUserGUID;
		public Guid EditingUserGUID
		{
			get { return _editingUserGUID; }
			set
			{
				_editingUserGUID = value;
				RaisePropertyChanged("EditingUserGUID");
			}
		}

		private XElement _metadataXML;
		public XElement MetadataXML
		{
			get { return _metadataXML; }
			set
			{
				_metadataXML = value;
				RaisePropertyChanged("MetadataXml");
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