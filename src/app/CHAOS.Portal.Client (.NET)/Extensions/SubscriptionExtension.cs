using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public class SubscriptionExtension : AExtension, ISubscriptionExtension
	{
		public IServiceCallState<Subscription> Get(Guid guid)
		{
			return CallService<Subscription>(HTTPMethod.GET, guid);
		}

		public IServiceCallState<Subscription> Create(string name)
		{
			return CallService<Subscription>(HTTPMethod.POST, name);
		}

		public IServiceCallState<ScalarResult> Update(Guid guid, string newName)
		{
			return CallService<ScalarResult>(HTTPMethod.POST, newName);
		}

		public IServiceCallState<ScalarResult> Delete(Guid guid)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, guid);
		}
	}
}