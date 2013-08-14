using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public class FolderTypeExtension : AExtension, IFolderTypeExtension
	{
		public IServiceCallState<PagedResult<FolderType>> Get(int? id, string name)
		{
			return CallService<PagedResult<FolderType>>(HTTPMethod.GET, id, name);
		}

		public IServiceCallState<PagedResult<FolderType>> Create(string name)
		{
			return CallService<PagedResult<FolderType>>(HTTPMethod.POST, name);
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