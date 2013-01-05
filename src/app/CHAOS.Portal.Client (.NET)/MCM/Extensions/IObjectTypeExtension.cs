using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IObjectTypeExtension
	{
		IServiceCallState<IServiceResult_MCM<ObjectType>> Get(int? id = null, string name = null);
		IServiceCallState<IServiceResult_MCM<ObjectType>> Create(string value);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Update(int id, string newValue);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Delete(int id);
	}
}