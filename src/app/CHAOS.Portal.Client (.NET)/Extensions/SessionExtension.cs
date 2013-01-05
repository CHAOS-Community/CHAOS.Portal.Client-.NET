using System;
using CHAOS.Events;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public class SessionExtension : AExtension, ISessionExtension, ISessionChangingExtension
	{
		public event EventHandler<DataEventArgs<Session>> SessionChanged = delegate { };

		public IServiceCallState<IServiceResult_Portal<Session>> Create()
		{
			var state = CallServiceWithoutSession<IServiceResult_Portal<Session>>(HTTPMethod.GET);

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
				SessionChanged(this, new DataEventArgs<Session>(e.Data.Portal.Data[0]));
		}

		private void UpdateCompleted(object sender, DataOperationEventArgs<IServiceResult_Portal<Session>> e)
		{
			((IServiceCallState<IServiceResult_Portal<Session>>)sender).OperationCompleted -= UpdateCompleted;

			if (e.Error == null && e.Data.Portal.Error == null && e.Data.Portal.Data.Count == 1) //TODO: Handle if there is less or more than one Session returned.
				SessionChanged(this, new DataEventArgs<Session>(e.Data.Portal.Data[0]));
		}

		private void DeleteCompleted(object sender, DataOperationEventArgs<IServiceResult_Portal<ScalarResult>> e)
		{
			((IServiceCallState<IServiceResult_Portal<ScalarResult>>)sender).OperationCompleted -= DeleteCompleted;

			if (e.Error == null && e.Data.Portal.Data[0].Value == 1) //TODO: Check and handle other values.
				SessionChanged(this, new DataEventArgs<Session>(null));
		}
	}
}