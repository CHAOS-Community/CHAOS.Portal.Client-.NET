using System;
using CHAOS.Events;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public class SessionExtension : AExtension, ISessionExtension, ISessionChangingExtension
	{
		public event EventHandler<DataEventArgs<Session>> SessionChanged = delegate { };

		public IServiceCallState<PagedResult<Session>> Create()
		{
			var state = CallServiceWithoutSession <PagedResult<Session>>(HTTPMethod.GET);

			state.OperationCompleted += CreateCompleted;

			return state;
		}

		public IServiceCallState<PagedResult<Session>> Get()
		{
			return CallService<PagedResult<Session>>(HTTPMethod.GET);
		}

		public IServiceCallState<PagedResult<Session>> Update()
		{
			var state = CallService<PagedResult<Session>>(HTTPMethod.POST);

			state.OperationCompleted += UpdateCompleted;

			return state;
		}

		public IServiceCallState<PagedResult<ScalarResult>> Delete()
		{
			var state = CallService<PagedResult<ScalarResult>>(HTTPMethod.GET);

			state.OperationCompleted += DeleteCompleted;

			return state;
		}

		private void CreateCompleted(object sender, DataEventArgs<ServiceResponse<PagedResult<Session>>> e)
		{
			((IServiceCallState<PagedResult<Session>>)sender).OperationCompleted -= CreateCompleted;

			if (e.Data.Error == null && e.Data.Body.Count == 1) //TODO: Handle if there is less or more than one Session returned.
				SessionChanged(this, new DataEventArgs<Session>(e.Data.Body.Results[0]));
		}

		private void UpdateCompleted(object sender, DataEventArgs<ServiceResponse<PagedResult<Session>>> e)
		{
			((IServiceCallState<PagedResult<Session>>)sender).OperationCompleted -= UpdateCompleted;

			if (e.Data.Error == null && e.Data.Body.Count == 1) //TODO: Handle if there is less or more than one Session returned.
				SessionChanged(this, new DataEventArgs<Session>(e.Data.Body.Results[0]));
		}

		private void DeleteCompleted(object sender, DataEventArgs<ServiceResponse<PagedResult<ScalarResult>>> e)
		{
			((IServiceCallState<PagedResult<ScalarResult>>)sender).OperationCompleted -= DeleteCompleted;

			if (e.Data.Error == null && e.Data.Body.Count == 1 && e.Data.Body.Results[0].Value == 1) //TODO: Check and handle other values.
				SessionChanged(this, new DataEventArgs<Session>(null));
		}
	}
}