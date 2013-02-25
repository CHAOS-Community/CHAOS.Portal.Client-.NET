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

		private Guid _metadataSchemaGuid;
		public Guid MetadataSchemaGuid
		{
			get { return _metadataSchemaGuid; }
			set
			{
				_metadataSchemaGuid = value;
				RaisePropertyChanged("MetadataSchemaGuid");
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

		private Guid _editingUserGuid;
		public Guid EditingUserGuid
		{
			get { return _editingUserGuid; }
			set
			{
				_editingUserGuid = value;
				RaisePropertyChanged("EditingUserGuid");
			}
		}

		private XElement _metadataXml;
		public XElement MetadataXml
		{
			get { return _metadataXml; }
			set
			{
				_metadataXml = value;
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