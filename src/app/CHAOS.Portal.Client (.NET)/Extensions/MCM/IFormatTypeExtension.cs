using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions.MCM
{
	public interface IFormatTypeExtension
	{
		IServiceCallState<IServiceResult_MCM<FormatType>> Get(int? id, string name);
		IServiceCallState<IServiceResult_MCM<FormatType>> Create(string name);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Update(int id, string newName);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Delete(int id);
	}
}