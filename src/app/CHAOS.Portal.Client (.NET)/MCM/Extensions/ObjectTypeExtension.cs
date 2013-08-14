using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public class ObjectTypeExtension : AExtension, IObjectTypeExtension
	{
		public IServiceCallState<PagedResult<ObjectType>> Get()
		{
			return CallService<PagedResult<ObjectType>>(HTTPMethod.GET);
		}

		public IServiceCallState<PagedResult<ObjectType>> Set(string name, uint? id)
		{
			return CallService<PagedResult<ObjectType>>(HTTPMethod.POST, name, id);
		}

		public IServiceCallState<PagedResult<ScalarResult>> Delete(uint id)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.GET, id);
		}
	}
}