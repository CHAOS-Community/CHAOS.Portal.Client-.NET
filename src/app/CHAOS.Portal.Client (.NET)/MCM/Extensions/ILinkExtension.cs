using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface ILinkExtension
	{
		IServiceCallState<ScalarResult> Create(Guid objectGUID, uint folderID);
		IServiceCallState<ScalarResult> Update(Guid objectGUID, uint folderID, uint newFolderID);
		IServiceCallState<ScalarResult> Delete(Guid objectGUID, uint folderID);
	}
}