using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IUploadExtension
	{
		IServiceCallState<IServiceResult_MCM<UploadToken>> Initiate(Guid objectGUID, uint formatID, ulong fileSize);
	}
}