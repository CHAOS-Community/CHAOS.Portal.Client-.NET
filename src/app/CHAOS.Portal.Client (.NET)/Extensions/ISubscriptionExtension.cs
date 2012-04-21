using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface ISubscriptionExtension
	{
		IServiceCallState<IServiceResult_Portal<Subscription>> Get(Guid guid);
		IServiceCallState<IServiceResult_Portal<Subscription>> Create(string name);
		IServiceCallState<IServiceResult_Portal<ScalarResult>> Update(Guid guid, string newName);
		IServiceCallState<IServiceResult_Portal<ScalarResult>> Delete(Guid guid);
	}
}