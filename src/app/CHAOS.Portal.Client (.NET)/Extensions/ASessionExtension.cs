using System;
using CHAOS.Events;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public abstract class ASessionExtension : AExtension, ISessionChangingExtension
	{
		public event EventHandler<DataEventArgs<Session>> SessionChanged = delegate { };

		protected IServiceCallState<PagedResult<Session>> SetSessionUpdatingState(IServiceCallState<PagedResult<Session>> state)
		{
			state.OperationCompleted += UpdateCompleted;
			return state;
		}

		protected IServiceCallState<PagedResult<ScalarResult>> SetSessionDeletingState(IServiceCallState<PagedResult<ScalarResult>> state)
		{
			state.OperationCompleted += DeleteCompleted;
			return state;
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