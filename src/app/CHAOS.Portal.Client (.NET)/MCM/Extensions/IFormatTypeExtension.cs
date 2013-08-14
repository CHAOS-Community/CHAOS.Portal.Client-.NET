using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IFormatTypeExtension
	{
		IServiceCallState<PagedResult<FormatType>> Get(int? id = null, string name = null);
		IServiceCallState<PagedResult<FormatType>> Create(string name);
		IServiceCallState<PagedResult<ScalarResult>> Update(int id, string newName);
		IServiceCallState<PagedResult<ScalarResult>> Delete(int id);
	}
}