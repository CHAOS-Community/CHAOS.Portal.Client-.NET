using System;
using System.Collections.Generic;
using CHAOS.Common.Utilities;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.Extensions.GeoLocator;
using CHAOS.Portal.Client.Extensions.MCM;
using CHAOS.Portal.Client.Extensions.Portal;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.Extension.GeoLocator;
using CHAOS.Portal.Client.Standard.Extension.MCM;
using CHAOS.Portal.Client.Standard.Extension.Portal;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard
{
	public class PortalClient : IPortalClient, IServiceCaller
	{
		private const string PARAMETER_NAME_SESSION_ID = "sessionID";
		private const string PARAMETER_NAME_FORMAT = "format";
		private const string PARAMETER_NAME_USE_HTTP_CODES = "useHTTPCodes";

		private const string PARAMETER_VALUE_FORMAT = "GXML";
		private const bool PARAMETER_VALUE_USE_HTTP_CODES = true;

		private const uint PROTOCOL_VERSION = 1;

		public event EventHandler SessionAcquired = delegate { };
		public event EventHandler ClientGUIDSet = delegate { };

		private readonly IServiceCallFactory _ServiceCallFactory;
		
		public string ServicePath { get; set; }

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

		private Guid? _ClientGUID;
		public Guid? ClientGUID
		{
			get { return _ClientGUID; }
			set
			{
				_ClientGUID = value;

				if (HasClientGUID)
					ClientGUIDSet(this, EventArgs.Empty);
			}
		}

		#region Extensions
		#region GeoLocator

		private readonly LocationExtension _Location;
		public ILocationExtension Location { get { return _Location; } }

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

		private readonly UserSettingsExtension _UserSettingsExtension;
		public IUserSettingsExtension UserSettings { get { return _UserSettingsExtension; } }

		#endregion
		#endregion

		public PortalClient(IServiceCallFactory serviceCallFactory)
		{
			_ServiceCallFactory = ArgumentUtilities.ValidateIsNotNull("serviceCallFactory", serviceCallFactory);

			_Location = new LocationExtension(this);

			_Folder = new FolderExtension(this);
			_FolderType = new FolderTypeExtension(this);
			_FormatType = new FormatTypeExtension(this);
			_Language = new LanguageExtension(this);
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
			_UserSettingsExtension = new UserSettingsExtension(this, this);
		}

		public IServiceCallState<T> CallService<T>(string extensionName, string commandName, IDictionary<string, object> parameters, HTTPMethod method, bool requiresSession) where T : class, IServiceResult
		{
			if(string.IsNullOrEmpty(ServicePath))
				throw new Exception("ServicePath must be set");
			
			if(requiresSession)
			{
				if (!HasSession)
					throw new Exception(string.Format("Session required before calling {0}/{1}", extensionName, commandName));

				parameters[PARAMETER_NAME_SESSION_ID] = CurrentSession.SessionID;
			}

			parameters[PARAMETER_NAME_FORMAT] = PARAMETER_VALUE_FORMAT;
			parameters[PARAMETER_NAME_USE_HTTP_CODES] = PARAMETER_VALUE_USE_HTTP_CODES;
			
			var call = _ServiceCallFactory.GetServiceCall<T>();

			call.Call(string.Format("{0}/{1}/{2}", ServicePath, extensionName, commandName), parameters, method); //Note: In theory call could complete before state is returned, consider refactoring.

			return call.State;
		}
	}
}