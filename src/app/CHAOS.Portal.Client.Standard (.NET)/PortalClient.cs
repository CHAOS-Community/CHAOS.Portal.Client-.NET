using System;
using System.Collections.Generic;
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
		private const bool PARAMETER_VALUE_USE_HTTP_CODES = true;

		private const uint PROTOCOL_VERSION = 4;

		public event EventHandler SessionAcquired = delegate { };
		public event EventHandler ClientGUIDSet = delegate { };

		private readonly IServiceCallFactory _serviceCallFactory;

		private string _servicePath = null;
		public string ServicePath
		{
			get { return _servicePath; }
			set
			{
				if(value != null && value.EndsWith("/"))
					_servicePath = value.Substring(0, value.Length - 1);
				else
					_servicePath = value;
			}
		}

		public bool HasSession { get { return CurrentSession != null; } }

		public Session CurrentSession
		{
			get { return _Session.Session; }
			set
			{
				if(value == null)
					return;
				_Session.Session = value;
				SessionAcquired(this, EventArgs.Empty);
			}
		}

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

		#region Extensions
		#region GeoLocator

		private readonly LocationExtension _location;
		public ILocationExtension Location { get { return _location; } }

		#endregion
		#region MCM

		private readonly FolderExtension _Folder;
		public IFolderExtension Folder { get { return _Folder; } }

		private readonly FolderTypeExtension _FolderType;
		public IFolderTypeExtension FolderType { get { return _FolderType; } }

		private readonly FormatTypeExtension _FormatType;
		public IFormatTypeExtension FormatType { get { return _FormatType; } }

		private readonly LanguageExtension _Language;
		public ILanguageExtension Language { get { return _Language; } }

		private readonly ILinkExtension _link;
		public ILinkExtension Link { get { return _link; } }

		private readonly MetadataExtension _Metadata;
		public IMetadataExtension Metadata { get { return _Metadata; } }

		private readonly MetadataSchemaExtension _MetadataSchema;
		public IMetadataSchemaExtension MetadataSchema { get { return _MetadataSchema; } }

		private readonly ObjectExtension _Object;
		public IObjectExtension Object { get { return _Object; } }

		private readonly ObjectRelationTypeExtension _ObjectRelationType;
		public IObjectRelationTypeExtension ObjectRelationType { get { return _ObjectRelationType; } }

		private readonly ObjectTypeExtension _ObjectType;
		public IObjectTypeExtension ObjectType { get { return _ObjectType; } }
		
		#endregion
		#region Portal

		private readonly ClientSettingsExtension _ClientSettings;
		public IClientSettingsExtension ClientSettings { get { return _ClientSettings; } }

		private readonly EmailPasswordExtension _EmailPassword;
		public IEmailPasswordExtension EmailPassword { get { return _EmailPassword; } }

		private readonly GroupExtension _Group;
		public IGroupExtension Group { get { return _Group; } }

		private readonly SessionExtension _Session;
		public ISessionExtension Session { get { return _Session; } }

		private readonly SecureCookieExtension _SecureCookie;
		public ISecureCookieExtension SecureCookie { get { return _SecureCookie; } }

		private readonly SubscriptionExtension _Subscription;
		public ISubscriptionExtension Subscription { get { return _Subscription; } }

		private readonly UserExtension _User;
		public IUserExtension User { get { return _User; } }

		private readonly UserSettingsExtension _userSettings;
		public IUserSettingsExtension UserSettings { get { return _userSettings; } }

		#endregion
		#region Statistics

		private readonly StatsObjectExtension _statsObject;
		public IStatsObjectExtension StatsObject { get { return _statsObject; } }

		#endregion
		#endregion

		public PortalClient(IServiceCallFactory serviceCallFactory)
		{
			_serviceCallFactory = ArgumentUtilities.ValidateIsNotNull("serviceCallFactory", serviceCallFactory);

			_location = new LocationExtension(this);

			_Folder = new FolderExtension(this);
			_FolderType = new FolderTypeExtension(this);
			_FormatType = new FormatTypeExtension(this);
			_Language = new LanguageExtension(this);
			_link = new LinkExtension(this);
			_Metadata = new MetadataExtension(this);
			_MetadataSchema = new MetadataSchemaExtension(this);
			_Object = new ObjectExtension(this);
			_ObjectRelationType = new ObjectRelationTypeExtension(this);
			_ObjectType = new ObjectTypeExtension(this);

			_ClientSettings = new ClientSettingsExtension(this);
			_EmailPassword = new EmailPasswordExtension(this);
			_Group = new GroupExtension(this);
			_Session = new SessionExtension(this, PROTOCOL_VERSION);
			_SecureCookie = new SecureCookieExtension(this);
			_Subscription = new SubscriptionExtension(this);
			_User = new UserExtension(this);
			_userSettings = new UserSettingsExtension(this, this);

			_statsObject = new StatsObjectExtension(this);
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

			call.Call(string.Format("{0}/{1}/{2}", ServicePath, extensionName, commandName), parameters, method); //Note: In theory call could complete before state is returned, consider refactoring.

			return call.State;
		}
	}
}