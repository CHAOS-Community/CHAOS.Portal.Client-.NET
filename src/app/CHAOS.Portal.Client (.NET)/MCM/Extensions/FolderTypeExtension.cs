using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public class FolderTypeExtension : AExtension, IFolderTypeExtension
	{
		public IServiceCallState<FolderType> Get(int? id, string name)
		{
			return CallService<FolderType>(HTTPMethod.GET, id, name);
		}

		public IServiceCallState<FolderType> Create(string name)
		{
			return CallService<FolderType>(HTTPMethod.POST, name);
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