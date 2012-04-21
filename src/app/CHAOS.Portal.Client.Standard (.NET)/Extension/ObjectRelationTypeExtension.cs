using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class ObjectRelationTypeExtension : Extension, IObjectRelationTypeExtension
	{
		public ObjectRelationTypeExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }

		public IServiceCallState<IServiceResult_MCM<ObjectRelationType>> Get(int? id, string name)
		{
			return CallService<IServiceResult_MCM<ObjectRelationType>>(HTTPMethod.GET, id, name);
		}

		public IServiceCallState<IServiceResult_MCM<ObjectRelationType>> Create(string name)
		{
			return CallService<IServiceResult_MCM<ObjectRelationType>>(HTTPMethod.POST, name);
		}

		public IServiceCallState<IServiceResult_MCM<ScalarResult>> Update(int id, string name)
		{
			return CallService<IServiceResult_MCM<ScalarResult>>(HTTPMethod.POST, id, name);
		}

		public IServiceCallState<IServiceResult_MCM<ScalarResult>> Delete(int id)
		{
			return CallService<IServiceResult_MCM<ScalarResult>>(HTTPMethod.GET, id);
		}
	}
}