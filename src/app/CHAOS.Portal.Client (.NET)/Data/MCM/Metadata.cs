using System;
using System.Xml.Linq;

namespace CHAOS.Portal.Client.Data.MCM
{
	public class Metadata : AData
	{
		private string _LanguageCode;
		public string LanguageCode
		{
			get { return _LanguageCode; }
			set
			{
				_LanguageCode = value;
				RaisePropertyChanged("LanguageCode");
			}
		}

		private Guid _MetadataSchemaGUID;
		public Guid MetadataSchemaGUID
		{
			get { return _MetadataSchemaGUID; }
			set
			{
				_MetadataSchemaGUID = value;
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

		private XElement _MetadataXML;
		public XElement MetadataXML
		{
			get { return _MetadataXML; }
			set
			{
				_MetadataXML = value;
				RaisePropertyChanged("MetadataXml");
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

		private DateTime _DateModified;
		public DateTime DateModified
		{
			get { return _DateModified; }
			set
			{
				_DateModified = value;
				RaisePropertyChanged("DateModified");
			}
		}
	}
}