using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Portal;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IGroupExtension
	{
		IServiceCallState<IServiceResult_Portal<Group>> Get(Guid guid);
		IServiceCallState<IServiceResult_Portal<Group>> Create(string name, int systemPermission);
		IServiceCallState<IServiceResult_Portal<ScalarResult>> Update(Guid guid, string newName, int newSystemPermission);
		IServiceCallState<IServiceResult_Portal<ScalarResult>> Delete(Guid guid);
	}
}