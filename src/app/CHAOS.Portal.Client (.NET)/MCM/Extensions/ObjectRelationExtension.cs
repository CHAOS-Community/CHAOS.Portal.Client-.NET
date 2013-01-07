using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public class ObjectRelationExtension : AExtension, IObjectRelationExtension
	{
		public IServiceCallState<ScalarResult> Create(Guid object1GUID, Guid object2GUID, uint objectRelationTypeID, int? sequence)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, object1GUID, object2GUID, objectRelationTypeID, sequence);
		}

		public IServiceCallState<ScalarResult> Delete(Guid object1GUID, Guid object2GUID, uint objectRelationTypeID)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, object1GUID, object2GUID, objectRelationTypeID);
		}
	}
}