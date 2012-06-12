using System;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.Extensions;

namespace CHAOS.Portal.Client
{
	/// <summary>
	/// Allows easy communication with a Portal service.
	/// </summary>
	public interface IPortalClient
	{
		/// <summary>
		/// SessionAcquired is raised when <code>CurrentSession</code> is set by calling Session/Get or by setting it directly..
		/// </summary>
		event EventHandler SessionAcquired;

		/// <summary>
		/// ClientGUIDSet is raised when <code>ClientGUID</code> is set.
		/// </summary>
		event EventHandler ClientGUIDSet;
		
		/// <summary>
		/// The base path to the Portal service, must be set before making any calls.
		/// </summary>
		string ServicePath { get; set; }

		/// <summary>
		/// Returns <code>true</code> if a session has been created (by calling <code>Session.Create()</code> or setting CurrentSession ).
		/// </summary>
		bool HasSession { get; }

		/// <summary>
		/// Gets or sets the session used by the portal client.
		/// </summary>
		Session CurrentSession { get; }

		/// <summary>
		/// Returns <code>true</code> if ClientGUID has been set.
		/// </summary>
		bool HasClientGUID { get; }

		/// <summary>
		/// Gets or sets the GUID used to identify the client.
		/// </summary>
		Guid? ClientGUID { get; set; }

		void UseExistingSession(Guid guid);

		#region GeoLocator Extensions

		/// <summary>
		/// A GeoLocator extension.
		/// </summary>
		ILocationExtension Location { get; }

		#endregion
		#region MCM Extension

		/// <summary>
		/// A MCM extension.
		/// </summary>
		IFolderExtension Folder { get; }

		/// <summary>
		/// A MCM extension.
		/// </summary>
		IFolderTypeExtension FolderType { get; }

		/// <summary>
		/// A MCM extension.
		/// </summary>
		IFormatTypeExtension FormatType { get; }

		/// <summary>
		/// A MCM extension.
		/// </summary>
		ILanguageExtension Language { get; }

		/// <summary>
		/// A MCM extension.
		/// </summary>
		ILinkExtension Link { get; }

		/// <summary>
		/// A MCM extension.
		/// </summary>
		IMetadataExtension Metadata { get; }

		/// <summary>
		/// A MCM extension.
		/// </summary>
		IMetadataSchemaExtension MetadataSchema { get; }

		/// <summary>
		/// A MCM extension
		/// </summary>
		IObjectExtension Object { get; }

		/// <summary>
		/// A MCM extension
		/// </summary>
		IObjectRelationExtension ObjectRelation { get; }

		/// <summary>
		/// A MCM extension.
		/// </summary>
		IObjectRelationTypeExtension ObjectRelationType{ get; }

		/// <summary>
		/// A MCM extension.
		/// </summary>
		IObjectTypeExtension ObjectType { get; }

		#endregion
		#region Portal Extensions

		/// <summary>
		/// A Portal extension that enables the use of serverside clientsettings.
		/// </summary>
		IClientSettingsExtension ClientSettings { get; }

		/// <summary>
		/// A Portal extension that enables authentication via email/password.
		/// </summary>
		IEmailPasswordExtension EmailPassword { get; }

		/// <summary>
		/// A Portal extension.
		/// </summary>
		IGroupExtension Group { get; }
		
		/// <summary>
		/// A Portal extension that handles sessions, is used to create the session that the <code>IPortalClient</code> uses.
		/// Creating a <code>Session</code> sets it as the session the <code>IPortalClient</code> uses.
		/// </summary>
		ISessionExtension Session { get; }

		ISecureCookieExtension SecureCookie { get; }

		/// <summary>
		/// A Portal extension.
		/// </summary>
		ISubscriptionExtension Subscription { get; }

		/// <summary>
		/// A Portal extension.
		/// </summary>
		IUserExtension User { get; }

		/// <summary>
		/// A Portal extension that enables the user of serverside user settings per client.
		/// </summary>
		IUserSettingsExtension UserSettings { get; }

		#endregion
		#region Statistics Extensions

		/// <summary>
		/// A Statistics extension.
		/// </summary>
		IStatsObjectExtension StatsObject { get; }

		/// <summary>
		/// A Statistics extension.
		/// </summary>
		IDayStatsObjectExtension DayStatsObject { get; }

		#endregion
	}
}