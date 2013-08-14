using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface ISubscriptionExtension
	{
		IServiceCallState<PagedResult<Subscription>> Get(Guid guid);
		IServiceCallState<PagedResult<Subscription>> Create(string name);
		IServiceCallState<PagedResult<ScalarResult>> Update(Guid guid, string newName);
		IServiceCallState<PagedResult<ScalarResult>> Delete(Guid guid);
	}
}