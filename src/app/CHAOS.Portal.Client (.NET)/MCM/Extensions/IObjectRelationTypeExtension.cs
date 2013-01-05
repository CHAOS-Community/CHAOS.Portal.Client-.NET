using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IObjectRelationTypeExtension
	{
		IServiceCallState<IServiceResult_MCM<ObjectRelationType>> Get(int? id = null, string value = null);
		IServiceCallState<IServiceResult_MCM<ObjectRelationType>> Create(string value);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Update(int id, string newValue);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Delete(int id);
	}
}