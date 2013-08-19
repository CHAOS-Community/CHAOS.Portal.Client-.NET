using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CHAOS.Events;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using HTTPMethod = CHAOS.Portal.Client.ServiceCall.HTTPMethod;

namespace CHAOS.Portal.Client.Standard
{
	public class PortalClient : IPortalClient, IServiceCaller
	{
		private const string PARAMETER_NAME_SESSION_ID = "sessionGUID";
		private const string PARAMETER_NAME_FORMAT = "format";
		private const string PARAMETER_NAME_USE_HTTP_CODES = "useHTTPCodes";

		private const string PARAMETER_VALUE_FORMAT = "XML2";
		private const bool PARAMETER_VALUE_USE_HTTP_CODES = false;

		private const uint PROTOCOL_VERSION = 6;

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

		public uint ProtocolVersion { get { return PROTOCOL_VERSION; } }
		public bool UseLatest { get; set; }

		public PortalClient() : this(new ServiceCallFactory())
		{
			
		}

		public PortalClient(IServiceCallFactory serviceCallFactory)
		{
			_serviceCallFactory = serviceCallFactory;
		}

		public void RegisterExtension(IExtension extension)
		{
			var sessionExtension = extension as ISessionChangingExtension;
			var clientGUIDDependentExtension = extension as IClientGUIDDependentExtension;

			if (sessionExtension != null)
				sessionExtension.SessionChanged += SessionChanged;

			if (clientGUIDDependentExtension != null)
			{
				if(!HasClientGUID)
					throw new Exception("ClientGUID not set");

				clientGUIDDependentExtension.ClientGUID = ClientGUID.Value;
			}
		}

		#region ClientGUID

		public event EventHandler ClientGUIDSet = delegate { };

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

		#endregion
		#region Session

		private bool _sessionAcquired;

		public event EventHandler SessionAcquired = delegate { };

		public bool HasSession { get { return CurrentSession != null; } }
		public Session CurrentSession { get; private set; }

		public void UseExistingSession(Guid guid)
		{
			CurrentSession = new Session { Guid = guid };

			this.Session().Update();
		}

		private void SessionChanged(object sender, DataEventArgs<Session> eventArgs)
		{
			CurrentSession = eventArgs.Data;

			if (CurrentSession == null)
			{
				_sessionAcquired = false;
			}
			else if (!_sessionAcquired)
			{
				_sessionAcquired = true;
				SessionAcquired(this, EventArgs.Empty);
			}
		}
		
		#endregion
		#region Call

		public IServiceCallState<T> CallService<T>(string extensionName, string commandName, IDictionary<string, object> parameters, HTTPMethod method, bool requiresSession) where T : class, IServiceBody
		{
			if (string.IsNullOrEmpty(ServicePath))
				throw new Exception("ServicePath must be set");

			if (requiresSession)
			{
				if (!HasSession)
					throw new Exception(string.Format("Session required before calling {0}/{1}", extensionName, commandName));

				parameters[PARAMETER_NAME_SESSION_ID] = CurrentSession.Guid;
			}

			parameters[PARAMETER_NAME_FORMAT] = PARAMETER_VALUE_FORMAT;
			parameters[PARAMETER_NAME_USE_HTTP_CODES] = PARAMETER_VALUE_USE_HTTP_CODES;

			var call = _serviceCallFactory.GetServiceCall<T>();

			call.Call(string.Format(UseLatest ? "{0}/latest/{2}/{3}" : "{0}/v{1}/{2}/{3}", ServicePath, ProtocolVersion, extensionName, commandName), parameters, method == HTTPMethod.GET ? Web.HTTPMethod.GET : Web.HTTPMethod.POST); //Note: In theory call could complete before state is returned, consider refactoring.

			return call.State;
		}

		#endregion
	}
}