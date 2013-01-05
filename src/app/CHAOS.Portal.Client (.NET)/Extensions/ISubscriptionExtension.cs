using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface ISubscriptionExtension
	{
		IServiceCallState<Subscription> Get(Guid guid);
		IServiceCallState<Subscription> Create(string name);
		IServiceCallState<ScalarResult> Update(Guid guid, string newName);
		IServiceCallState<ScalarResult> Delete(Guid guid);
	}
}