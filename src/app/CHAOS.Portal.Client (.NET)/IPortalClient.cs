using System;
using CHAOS.Portal.Client.Data.Portal;

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

		/// <summary>
		/// Gets the protocol version used by the client
		/// </summary>
		uint ProtocolVersion { get; }

		/// <summary>
		/// Gives the client a session GUID to use
		/// </summary>
		/// <param name="guid">A valid session GUID</param>
		void UseExistingSession(Guid guid);
	}
}