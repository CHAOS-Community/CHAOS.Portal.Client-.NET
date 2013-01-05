using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IObjectRelationTypeExtension
	{
		IServiceCallState<ObjectRelationType> Get(int? id = null, string value = null);
		IServiceCallState<ObjectRelationType> Create(string value);
		IServiceCallState<ScalarResult> Update(int id, string newValue);
		IServiceCallState<ScalarResult> Delete(int id);
	}
}