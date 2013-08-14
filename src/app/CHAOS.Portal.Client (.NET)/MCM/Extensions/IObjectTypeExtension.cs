using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IObjectTypeExtension
	{
		IServiceCallState<PagedResult<ObjectType>> Get();
		IServiceCallState<PagedResult<ObjectType>> Set(string name, uint? id = null);
		IServiceCallState<PagedResult<ScalarResult>> Delete(uint id);
	}
}