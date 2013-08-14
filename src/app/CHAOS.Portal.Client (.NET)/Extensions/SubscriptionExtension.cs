using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public class SubscriptionExtension : AExtension, ISubscriptionExtension
	{
		public IServiceCallState<PagedResult<Subscription>> Get(Guid guid)
		{
			return CallService<PagedResult<Subscription>>(HTTPMethod.GET, guid);
		}

		public IServiceCallState<PagedResult<Subscription>> Create(string name)
		{
			return CallService<PagedResult<Subscription>>(HTTPMethod.POST, name);
		}

		public IServiceCallState<PagedResult<ScalarResult>> Update(Guid guid, string newName)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.POST, newName);
		}

		public IServiceCallState<PagedResult<ScalarResult>> Delete(Guid guid)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.GET, guid);
		}
	}
}