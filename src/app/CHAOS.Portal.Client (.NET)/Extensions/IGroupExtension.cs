using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IGroupExtension
	{
		IServiceCallState<Group> Get(Guid? guid = null);
		IServiceCallState<Group> Create(string name, int systemPermission);
		IServiceCallState<ScalarResult> Update(Guid guid, string newName, int newSystemPermission);
		IServiceCallState<ScalarResult> Delete(Guid guid);
	}
}