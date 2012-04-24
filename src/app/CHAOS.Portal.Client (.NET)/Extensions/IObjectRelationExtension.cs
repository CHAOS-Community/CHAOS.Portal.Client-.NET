using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IObjectRelationExtension
	{
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Create(UUID object1GUID, UUID object2GUID, uint objectRelationTypeID, int? sequence);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Delete(UUID object1GUID, UUID object2GUID, uint objectRelationTypeID);
	}
}