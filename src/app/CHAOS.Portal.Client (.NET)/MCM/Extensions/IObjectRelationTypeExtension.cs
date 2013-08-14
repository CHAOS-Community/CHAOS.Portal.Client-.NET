using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IObjectRelationTypeExtension
	{
		IServiceCallState<PagedResult<ObjectRelationType>> Get(int? id = null, string value = null);
		IServiceCallState<PagedResult<ObjectRelationType>> Create(string value);
		IServiceCallState<PagedResult<ScalarResult>> Update(int id, string newValue);
		IServiceCallState<PagedResult<ScalarResult>> Delete(int id);
	}
}