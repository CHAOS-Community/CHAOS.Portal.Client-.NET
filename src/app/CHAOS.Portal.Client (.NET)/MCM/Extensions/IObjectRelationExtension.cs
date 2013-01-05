using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IObjectRelationExtension
	{
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Create(Guid object1GUID, Guid object2GUID, uint objectRelationTypeID, int? sequence);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Delete(Guid object1GUID, Guid object2GUID, uint objectRelationTypeID);
	}
}