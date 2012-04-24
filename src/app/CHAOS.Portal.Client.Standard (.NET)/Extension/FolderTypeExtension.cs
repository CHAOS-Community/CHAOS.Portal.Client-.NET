using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class FolderTypeExtension : AExtension, IFolderTypeExtension
	{
		public FolderTypeExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }

		public IServiceCallState<IServiceResult_MCM<FolderType>> Get(int? id, string name)
		{
			return CallService<IServiceResult_MCM<FolderType>>(HTTPMethod.GET, id, name);
		}

		public IServiceCallState<IServiceResult_MCM<FolderType>> Create(string name)
		{
			return CallService<IServiceResult_MCM<FolderType>>(HTTPMethod.POST, name);
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