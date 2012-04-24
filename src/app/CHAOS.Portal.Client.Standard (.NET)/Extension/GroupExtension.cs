using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class GroupExtension : AExtension, IGroupExtension
	{
		public GroupExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }

		public IServiceCallState<IServiceResult_Portal<Group>> Get(Guid guid)
		{
			return CallService<IServiceResult_Portal<Group>>(HTTPMethod.GET, guid);
		}

		public IServiceCallState<IServiceResult_Portal<Group>> Create(string name, int systemPermission)
		{
			return CallService<IServiceResult_Portal<Group>>(HTTPMethod.POST, name, systemPermission);
		}

		public IServiceCallState<IServiceResult_Portal<ScalarResult>> Update(Guid guid, string newName, int newSystemPermission)
		{
			return CallService<IServiceResult_Portal<ScalarResult>>(HTTPMethod.POST, guid, newName, newSystemPermission);
		}

		public IServiceCallState<IServiceResult_Portal<ScalarResult>> Delete(Guid guid)
		{
			return CallService<IServiceResult_Portal<ScalarResult>>(HTTPMethod.GET, guid);
		}
	}
}