using System;
using CHAOS.Events;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public class SessionExtension : AExtension, ISessionExtension, ISessionChangingExtension
	{
		public event EventHandler<DataEventArgs<Session>> SessionChanged = delegate { };

		public IServiceCallState<Session> Create()
		{
			var state = CallServiceWithoutSession<Session>(HTTPMethod.GET);

			state.OperationCompleted += CreateCompleted;

			return state;
		}

		public IServiceCallState<Session> Get()
		{
			return CallService<Session>(HTTPMethod.GET);
		}

		public IServiceCallState<Session> Update()
		{
			var state = CallService<Session>(HTTPMethod.POST);

			state.OperationCompleted += UpdateCompleted;

			return state;
		}

		public IServiceCallState<ScalarResult> Delete()
		{
			var state = CallService<ScalarResult>(HTTPMethod.GET);

			state.OperationCompleted += DeleteCompleted;

			return state;
		}

		private void CreateCompleted(object sender, DataEventArgs<ServiceResponse<Session>> e)
		{
			((IServiceCallState<Session>)sender).OperationCompleted -= CreateCompleted;

			if (e.Data.Error == null && e.Data.Result.Count == 1) //TODO: Handle if there is less or more than one Session returned.
				SessionChanged(this, new DataEventArgs<Session>(e.Data.Result.Results[0]));
		}

		private void UpdateCompleted(object sender, DataEventArgs<ServiceResponse<Session>> e)
		{
			((IServiceCallState<Session>)sender).OperationCompleted -= UpdateCompleted;

			if (e.Data.Error == null && e.Data.Result.Count == 1) //TODO: Handle if there is less or more than one Session returned.
				SessionChanged(this, new DataEventArgs<Session>(e.Data.Result.Results[0]));
		}

		private void DeleteCompleted(object sender, DataEventArgs<ServiceResponse<ScalarResult>> e)
		{
			((IServiceCallState<ScalarResult>)sender).OperationCompleted -= DeleteCompleted;

			if (e.Data.Error == null && e.Data.Result.Count == 1 && e.Data.Result.Results[0].Value == 1) //TODO: Check and handle other values.
				SessionChanged(this, new DataEventArgs<Session>(null));
		}
	}
}