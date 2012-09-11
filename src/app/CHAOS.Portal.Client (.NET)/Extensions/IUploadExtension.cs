using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IUploadExtension
	{
		IServiceCallState<IServiceResult_Portal<UploadToken>> Initiate(Guid objectGUID, uint formatID, ulong fileSize, bool supportMultipleChunks);
		IServiceCallState<IServiceResult_Portal<ScalarResult>> Transfer(string uploadID, uint chunkIndex, byte[] fileData);
	}
}