using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class SubscriptionExtension : AExtension, ISubscriptionExtension
	{
		public SubscriptionExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }

		public IServiceCallState<IServiceResult_Portal<Subscription>> Get(Guid guid)
		{
			return CallService<IServiceResult_Portal<Subscription>>(HTTPMethod.GET, guid);
		}

		public IServiceCallState<IServiceResult_Portal<Subscription>> Create(string name)
		{
			return CallService<IServiceResult_Portal<Subscription>>(HTTPMethod.POST, name);
		}

		public IServiceCallState<IServiceResult_Portal<ScalarResult>> Update(Guid guid, string newName)
		{
			return CallService<IServiceResult_Portal<ScalarResult>>(HTTPMethod.POST, newName);
		}

		public IServiceCallState<IServiceResult_Portal<ScalarResult>> Delete(Guid guid)
		{
			return CallService<IServiceResult_Portal<ScalarResult>>(HTTPMethod.GET, guid);
		}
	}
}