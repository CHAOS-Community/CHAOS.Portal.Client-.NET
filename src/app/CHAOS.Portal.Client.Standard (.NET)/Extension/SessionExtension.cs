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
		private readonly uint _protocolVersion;

		public Session Session { get; set; }

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

			state.OperationCompleted += GetCompleted;

			return state;
		}

		public IServiceCallState<IServiceResult_Portal<Session>> Get()
		{
			return CallService<IServiceResult_Portal<Session>>(HTTPMethod.GET);
		}

		private void GetCompleted(object sender, DataOperationEventArgs<IServiceResult_Portal<Session>> e)
		{
			((IServiceCallState<IServiceResult_Portal<Session>>)sender).OperationCompleted -= GetCompleted;

			if(e.Error == null)
				Session = e.Data.Portal.Data[0]; //TODO: Check and handle if there is less or more than one Session returned.
		}

		public IServiceCallState<IServiceResult_Portal<Session>> Update()
		{
			return CallService<IServiceResult_Portal<Session>>(HTTPMethod.POST);
		}

		public IServiceCallState<IServiceResult_Portal<ScalarResult>> Delete()
		{
			var state = CallService<IServiceResult_Portal<ScalarResult>>(HTTPMethod.GET);

			state.OperationCompleted += DeleteCompleted;

			return state;
		}

		private void DeleteCompleted(object sender, DataOperationEventArgs<IServiceResult_Portal<ScalarResult>> e)
		{
			((IServiceCallState<IServiceResult_Portal<ScalarResult>>)sender).OperationCompleted -= DeleteCompleted;

			if (e.Error == null && e.Data.Portal.Data[0].Value == 1) //TODO: Check and handle other values.
				Session = null;
		}
	}
}