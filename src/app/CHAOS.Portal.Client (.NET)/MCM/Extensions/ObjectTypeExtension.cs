using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public class ObjectTypeExtension : AExtension, IObjectTypeExtension
	{
		public IServiceCallState<ObjectType> Get(int? id, string name)
		{
			return CallService<ObjectType>(HTTPMethod.GET, id, name);
		}

		public IServiceCallState<ObjectType> Create(string name)
		{
			return CallService<ObjectType>(HTTPMethod.POST, name);
		}

		public IServiceCallState<ScalarResult> Update(int id, string name)
		{
			return CallService<ScalarResult>(HTTPMethod.POST, id, name);
		}

		public IServiceCallState<ScalarResult> Delete(int id)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, id);
		}
	}
}