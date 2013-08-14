using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public class FormatTypeExtension : AExtension, IFormatTypeExtension
	{
		public IServiceCallState<PagedResult<FormatType>> Get(int? id, string name)
		{
			return CallService<PagedResult<FormatType>>(HTTPMethod.GET, id, name);
		}

		public IServiceCallState<PagedResult<FormatType>> Create(string name)
		{
			return CallService<PagedResult<FormatType>>(HTTPMethod.POST, name);
		}

		public IServiceCallState<PagedResult<ScalarResult>> Update(int id, string name)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.POST, id, name);
		}

		public IServiceCallState<PagedResult<ScalarResult>> Delete(int id)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.GET, id);
		}
	}
}