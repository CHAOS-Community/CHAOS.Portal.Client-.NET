using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IObjectTypeExtension
	{
		IServiceCallState<ObjectType> Get();
		IServiceCallState<ObjectType> Set(string name, uint? id = null);
		IServiceCallState<ScalarResult> Delete(uint id);
	}
}