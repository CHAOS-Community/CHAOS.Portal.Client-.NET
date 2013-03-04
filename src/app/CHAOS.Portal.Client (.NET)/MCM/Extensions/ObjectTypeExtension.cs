using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public class ObjectTypeExtension : AExtension, IObjectTypeExtension
	{
		public IServiceCallState<ObjectType> Get()
		{
			return CallService<ObjectType>(HTTPMethod.GET);
		}

		public IServiceCallState<ObjectType> Set(string name, uint? id)
		{
			return CallService<ObjectType>(HTTPMethod.POST, name, id);
		}

		public IServiceCallState<ScalarResult> Delete(uint id)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, id);
		}
	}
}