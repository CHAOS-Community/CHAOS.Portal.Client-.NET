using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public class LinkExtension : AExtension, ILinkExtension
	{
		public IServiceCallState<ScalarResult> Create(Guid objectGUID, uint folderID)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, objectGUID, folderID);
		}

		public IServiceCallState<ScalarResult> Update(Guid objectGUID, uint folderID, uint newFolderID)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, objectGUID, folderID, newFolderID);
		}

		public IServiceCallState<ScalarResult> Delete(Guid objectGUID, uint folderID)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, objectGUID, folderID);
		}
	}
}