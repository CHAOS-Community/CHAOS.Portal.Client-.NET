using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IUploadExtension
	{
		IServiceCallState<IServiceResult_Portal<ScalarResult>> Initiate(Guid objectGUID, uint formatID, uint chunkSize, uint noOfChunks);
	}
}