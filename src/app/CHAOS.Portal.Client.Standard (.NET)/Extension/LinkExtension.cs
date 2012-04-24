using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class LinkExtension : AExtension, ILinkExtension
	{
		public LinkExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }

		public IServiceCallState<IServiceResult_MCM<ScalarResult>> Create(Guid objectGUID, uint folderID)
		{
			return CallService<IServiceResult_MCM<ScalarResult>>(HTTPMethod.GET, objectGUID, folderID);
		}

		public IServiceCallState<IServiceResult_MCM<ScalarResult>> Update(Guid objectGUID, uint folderID, uint newFolderID)
		{
			return CallService<IServiceResult_MCM<ScalarResult>>(HTTPMethod.GET, objectGUID, folderID, newFolderID);
		}

		public IServiceCallState<IServiceResult_MCM<ScalarResult>> Delete(Guid objectGUID, uint folderID)
		{
			return CallService<IServiceResult_MCM<ScalarResult>>(HTTPMethod.GET, objectGUID, folderID);
		}
	}
}