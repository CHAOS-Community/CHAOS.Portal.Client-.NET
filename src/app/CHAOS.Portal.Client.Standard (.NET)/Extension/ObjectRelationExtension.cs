using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class ObjectRelationExtension : AExtension, IObjectRelationExtension
	{
		public ObjectRelationExtension(IServiceCaller serviceCaller) : base(serviceCaller) {}

		public IServiceCallState<IServiceResult_MCM<ScalarResult>> Create(Guid object1GUID, Guid object2GUID, uint objectRelationTypeID, int? sequence)
		{
			return CallService<IServiceResult_MCM<ScalarResult>>(HTTPMethod.GET, object1GUID, object2GUID, objectRelationTypeID, sequence);
		}

		public IServiceCallState<IServiceResult_MCM<ScalarResult>> Delete(Guid object1GUID, Guid object2GUID, uint objectRelationTypeID)
		{
			return CallService<IServiceResult_MCM<ScalarResult>>(HTTPMethod.GET, object1GUID, object2GUID, objectRelationTypeID);
		}
	}
}