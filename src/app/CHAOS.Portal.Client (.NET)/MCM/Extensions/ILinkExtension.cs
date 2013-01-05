using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface ILinkExtension
	{
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Create(Guid objectGUID, uint folderID);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Update(Guid objectGUID, uint folderID, uint newFolderID);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Delete(Guid objectGUID, uint folderID);
	}
}