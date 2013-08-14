using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IGroupExtension
	{
		IServiceCallState<PagedResult<Group>> Get(Guid? guid = null);
		IServiceCallState<PagedResult<Group>> Create(string name, int systemPermission);
		IServiceCallState<PagedResult<ScalarResult>> Update(Guid guid, string newName, int newSystemPermission);
		IServiceCallState<PagedResult<ScalarResult>> Delete(Guid guid);
	}
}