using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IFormatTypeExtension
	{
		IServiceCallState<FormatType> Get(int? id = null, string name = null);
		IServiceCallState<FormatType> Create(string name);
		IServiceCallState<ScalarResult> Update(int id, string newName);
		IServiceCallState<ScalarResult> Delete(int id);
	}
}