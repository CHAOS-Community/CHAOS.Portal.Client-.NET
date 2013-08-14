using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface ILinkExtension
	{
		IServiceCallState<PagedResult<ScalarResult>> Create(Guid objectGUID, uint folderID);
		IServiceCallState<PagedResult<ScalarResult>> Update(Guid objectGUID, uint folderID, uint newFolderID);
		IServiceCallState<PagedResult<ScalarResult>> Delete(Guid objectGUID, uint folderID);
	}
}