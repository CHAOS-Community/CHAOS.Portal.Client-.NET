using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.Standard.Extension;
using CHAOS.Utilities;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard
{
	public class PortalClient : IPortalClient, IServiceCaller
	{
		private const string PARAMETER_NAME_SESSION_ID = "sessionGUID";
		private const string PARAMETER_NAME_FORMAT = "format";
		private const string PARAMETER_NAME_USE_HTTP_CODES = "useHTTPCodes";

		private const string PARAMETER_VALUE_FORMAT = "XML";
		private const bool PARAMETER_VALUE_USE_HTTP_CODES = false;

		private const uint PROTOCOL_VERSION = 5;

		private bool _sessionAcquired;

		public event EventHandler SessionAcquired = delegate { };
		public event EventHandler ClientGUIDSet = delegate { };

		private readonly IServiceCallFactory _serviceCallFactory;

		private string _servicePath;
		public string ServicePath
		{
			get { return _servicePath; }
			set
			{
				if(new Regex("/v\\d+(?:/|$)").IsMatch(value))
					throw new Exception("Protocol version should not be part of service path");

				if(value != null && value.EndsWith("/"))
					_servicePath = value.Substring(0, value.Length - 1);
				else
					_servicePath = value;
			}
		}

		public bool HasSession { get { return CurrentSession != null; } }

		public Session CurrentSession { get { return _session.Session; } }

		public bool HasClientGUID
		{
			get { return ClientGUID.HasValue; }
		}

		private Guid? _clientGUID;
		public Guid? ClientGUID
		{
			get { return _clientGUID; }
			set
			{
				_clientGUID = value;

				if (HasClientGUID)
					ClientGUIDSet(this, EventArgs.Empty);
			}
		}

		public uint ProtocolVersion { get { return PROTOCOL_VERSION; } }

		public bool UseLatest { get; set; }

		#region Extensions
		#region GeoLocator

		private readonly ILocationExtension _location;
		public ILocationExtension Location { get { return _location; } }

		#endregion
		#region MCM

        private readonly IFileExtension _file;
        public IFileExtension File { get { return _file; } }

		private readonly IFolderExtension _folder;
		public IFolderExtension Folder { get { return _folder; } }

		private readonly IFolderTypeExtension _folderType;
		public IFolderTypeExtension FolderType { get { return _folderType; } }

		private readonly IFormatTypeExtension _formatType;
		public IFormatTypeExtension FormatType { get { return _formatType; } }

		private readonly ILanguageExtension _language;
		public ILanguageExtension Language { get { return _language; } }

		private readonly ILinkExtension _link;
		public ILinkExtension Link { get { return _link; } }

		private readonly IMetadataExtension _metadata;
		public IMetadataExtension Metadata { get { return _metadata; } }

		private readonly IMetadataSchemaExtension _metadataSchema;
		public IMetadataSchemaExtension MetadataSchema { get { return _metadataSchema; } }

		private readonly IObjectExtension _object;
		public IObjectExtension Object { get { return _object; } }

		private readonly IObjectRelationExtension _objectRelation;
		public IObjectRelationExtension ObjectRelation { get { return _objectRelation; } }

		private readonly IObjectRelationTypeExtension _objectRelationType;
		public IObjectRelationTypeExtension ObjectRelationType { get { return _objectRelationType; } }

		private readonly IObjectTypeExtension _objectType;
		public IObjectTypeExtension ObjectType { get { return _objectType; } }
		
		#endregion
		#region Portal

		private readonly IClientSettingsExtension _clientSettings;
		public IClientSettingsExtension ClientSettings { get { return _clientSettings; } }

		private readonly IEmailPasswordExtension _emailPassword;
		public IEmailPasswordExtension EmailPassword { get { return _emailPassword; } }

		private readonly IGroupExtension _group;
		public IGroupExtension Group { get { return _group; } }

		private readonly SessionExtension _session;
		public ISessionExtension Session { get { return _session; } }

		private readonly ISecureCookieExtension _secureCookie;
		public ISecureCookieExtension SecureCookie { get { return _secureCookie; } }

		private readonly ISubscriptionExtension _subscription;
		public ISubscriptionExtension Subscription { get { return _subscription; } }

		private readonly IUserExtension _user;
		public IUserExtension User { get { return _user; } }

		private readonly IUserSettingsExtension _userSettings;
		public IUserSettingsExtension UserSettings { get { return _userSettings; } }

		private readonly IUploadExtension _upload;
		public IUploadExtension Upload { get { return _upload; } }

		#endregion
		#region Statistics

		private readonly IStatsObjectExtension _statsObject;
		public IStatsObjectExtension StatsObject { get { return _statsObject; } }

		private readonly IDayStatsObjectExtension _dayStatsObject;
		public IDayStatsObjectExtension DayStatsObject { get { return _dayStatsObject; } }

		#endregion
		#region Indexing

		private readonly IndexExtension _index;
		public IIndexExtension Index { get { return _index; } }
		
		#endregion
		#endregion

		public PortalClient(IServiceCallFactory serviceCallFactory)
		{
			_serviceCallFactory = ArgumentUtilities.ValidateIsNotNull("serviceCallFactory", serviceCallFactory);

			_location = new LocationExtension(this);

            _file = new FileExtension(this);
			_folder = new FolderExtension(this);
			_folderType = new FolderTypeExtension(this);
			_formatType = new FormatTypeExtension(this);
			_language = new LanguageExtension(this);
			_link = new LinkExtension(this);
			_metadata = new MetadataExtension(this);
			_metadataSchema = new MetadataSchemaExtension(this);
			_object = new ObjectExtension(this);
			_objectRelation = new ObjectRelationExtension(this);
			_objectRelationType = new ObjectRelationTypeExtension(this);
			_objectType = new ObjectTypeExtension(this);

			_clientSettings = new ClientSettingsExtension(this);
			_emailPassword = new EmailPasswordExtension(this);
			_group = new GroupExtension(this);
			_session = new SessionExtension(this, PROTOCOL_VERSION);
			_secureCookie = new SecureCookieExtension(this);
			_subscription = new SubscriptionExtension(this);
			_user = new UserExtension(this);
			_userSettings = new UserSettingsExtension(this, this);
			_upload = new UploadExtension(this);

			_statsObject = new StatsObjectExtension(this);
			_dayStatsObject = new DayStatsObjectExtension(this);

			_index = new IndexExtension(this);

			_session.SessionChanged += SessionOnSessionChanged;
		}

		public void UseExistingSession(Guid guid)
		{
			_session.Session = new Session { SessionGUID = guid };
			Session.Update();
		}

		private void SessionOnSessionChanged(object sender, EventArgs eventArgs)
		{
			if(CurrentSession == null)
			{
				_sessionAcquired = false;
			}
			else if(!_sessionAcquired)
			{
				_sessionAcquired = true;
				SessionAcquired(this, EventArgs.Empty);
			}
		}

		public IServiceCallState<T> CallService<T>(string extensionName, string commandName, IDictionary<string, object> parameters, HTTPMethod method, bool requiresSession) where T : class, IServiceResult
		{
			if(string.IsNullOrEmpty(ServicePath))
				throw new Exception("ServicePath must be set");
			
			if(requiresSession)
			{
				if (!HasSession)
					throw new Exception(string.Format("Session required before calling {0}/{1}", extensionName, commandName));

				parameters[PARAMETER_NAME_SESSION_ID] = CurrentSession.SessionGUID;
			}

			parameters[PARAMETER_NAME_FORMAT] = PARAMETER_VALUE_FORMAT;
			parameters[PARAMETER_NAME_USE_HTTP_CODES] = PARAMETER_VALUE_USE_HTTP_CODES;
			
			var call = _serviceCallFactory.GetServiceCall<T>();

			call.Call(string.Format( UseLatest ? "{0}/latest/{2}/{3}" : "{0}/v{1}/{2}/{3}", ServicePath, ProtocolVersion, extensionName, commandName), parameters, method); //Note: In theory call could complete before state is returned, consider refactoring.

			return call.State;
		}
	}
}