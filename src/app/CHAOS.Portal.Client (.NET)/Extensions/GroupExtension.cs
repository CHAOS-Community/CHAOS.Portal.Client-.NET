using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public class GroupExtension : AExtension, IGroupExtension
	{
		public IServiceCallState<Group> Get(Guid? guid)
		{
			return CallService<Group>(HTTPMethod.GET, guid);
		}

		public IServiceCallState<Group> Create(string name, int systemPermission)
		{
			return CallService<Group>(HTTPMethod.POST, name, systemPermission);
		}

		public IServiceCallState<ScalarResult> Update(Guid guid, string newName, int newSystemPermission)
		{
			return CallService<ScalarResult>(HTTPMethod.POST, guid, newName, newSystemPermission);
		}

		public IServiceCallState<ScalarResult> Delete(Guid guid)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, guid);
		}
	}
}