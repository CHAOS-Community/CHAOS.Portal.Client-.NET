using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public class GroupExtension : AExtension, IGroupExtension
	{
		public IServiceCallState<PagedResult<Group>> Get(Guid? guid)
		{
			return CallService<PagedResult<Group>>(HTTPMethod.GET, guid);
		}

		public IServiceCallState<PagedResult<Group>> Create(string name, int systemPermission)
		{
			return CallService<PagedResult<Group>>(HTTPMethod.POST, name, systemPermission);
		}

		public IServiceCallState<PagedResult<ScalarResult>> Update(Guid guid, string newName, int newSystemPermission)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.POST, guid, newName, newSystemPermission);
		}

		public IServiceCallState<PagedResult<ScalarResult>> Delete(Guid guid)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.GET, guid);
		}
	}
}