using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Upload;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IUploadExtension
	{
		IServiceCallState<IServiceResult_Upload<UploadToken>> Initiate(Guid objectGUID, uint formatID, ulong fileSize, bool supportMultipleChunks);
		IServiceCallState<IServiceResult_Upload<ScalarResult>> Transfer(string uploadID, uint chunkIndex, byte[] fileData);
	}
}