﻿using CHAOS.Portal.Client.Managers;
using CHAOS.Portal.Client.Managers.Data;
using CHAOS.Portal.Client.Standard.Managers.Data;
using CHAOS.Portal.Client.Extensions;

namespace CHAOS.Portal.Client.Standard.Managers
{
	public class ViewManager : IViewManager
	{
		private readonly IPortalClient _client;

		public ViewManager(IPortalClient client)
		{
			_client = client;
		}

		public IManagerResult<T> Get<T>(string view, string query, string facet, string sort, string filter, uint pageSize) where T : class
		{
			var result = new ManagerResult<T>(pageSize,
				(index, managerResult) => _client.View().Get<T>(view, query, facet, sort, filter, index, pageSize).InvokeFeedbackOnDispatcher().WithCallback((response, token) =>
				{
					if(response.Error != null)
						return;
					managerResult.TotalCount = response.Body.TotalCount;
					if(response.Body.Count != 0)
						managerResult.AddResult(index, response.Body.Results);
				}));

			return result;
		}
	}
}