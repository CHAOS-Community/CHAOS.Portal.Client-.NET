using System;
using CHAOS.Events;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class SessionExtension : AExtension, ISessionExtension
	{
		public event EventHandler SessionChanged = delegate { };

		private readonly uint _protocolVersion;

		private Session _session;
		public Session Session
		{
			get { return _session; }
			set
			{
				_session = value;
				SessionChanged(this, EventArgs.Empty);
			}
		}

		public SessionExtension(IServiceCaller serviceCaller, uint protocolVersion) : base(serviceCaller)
		{
			_protocolVersion = protocolVersion;
		}

		public IServiceCallState<IServiceResult_Portal<Session>> Create()
		{
			return Create(_protocolVersion);
		}

		private IServiceCallState<IServiceResult_Portal<Session>> Create(uint protocolVersion)
		{
			var state = CallServiceWithoutSession<IServiceResult_Portal<Session>>(HTTPMethod.GET, protocolVersion);

			state.OperationCompleted += CreateCompleted;

			return state;
		}

		public IServiceCallState<IServiceResult_Portal<Session>> Get()
		{
			return CallService<IServiceResult_Portal<Session>>(HTTPMethod.GET);
		}

		public IServiceCallState<IServiceResult_Portal<Session>> Update()
		{
			var state = CallService<IServiceResult_Portal<Session>>(HTTPMethod.POST);

			state.OperationCompleted += UpdateCompleted;

			return state;
		}

		public IServiceCallState<IServiceResult_Portal<ScalarResult>> Delete()
		{
			var state = CallService<IServiceResult_Portal<ScalarResult>>(HTTPMethod.GET);

			state.OperationCompleted += DeleteCompleted;

			return state;
		}

		private void CreateCompleted(object sender, DataOperationEventArgs<IServiceResult_Portal<Session>> e)
		{
			((IServiceCallState<IServiceResult_Portal<Session>>)sender).OperationCompleted -= CreateCompleted;

			if (e.Error == null && e.Data.Portal.Error == null && e.Data.Portal.Data.Count == 1) //TODO: Handle if there is less or more than one Session returned.
				Session = e.Data.Portal.Data[0];
		}

		private void UpdateCompleted(object sender, DataOperationEventArgs<IServiceResult_Portal<Session>> e)
		{
			((IServiceCallState<IServiceResult_Portal<Session>>)sender).OperationCompleted -= UpdateCompleted;

			if (e.Error == null && e.Data.Portal.Error == null && e.Data.Portal.Data.Count == 1) //TODO: Handle if there is less or more than one Session returned.
				Session = e.Data.Portal.Data[0];
		}

		private void DeleteCompleted(object sender, DataOperationEventArgs<IServiceResult_Portal<ScalarResult>> e)
		{
			((IServiceCallState<IServiceResult_Portal<ScalarResult>>)sender).OperationCompleted -= DeleteCompleted;

			if (e.Error == null && e.Data.Portal.Data[0].Value == 1) //TODO: Check and handle other values.
				Session = null;
		}
	}
}