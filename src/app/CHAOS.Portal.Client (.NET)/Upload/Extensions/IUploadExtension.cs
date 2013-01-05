using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Upload.Data;

namespace CHAOS.Portal.Client.Upload.Extensions
{
	public interface IUploadExtension
	{
		IServiceCallState<IServiceResult_Upload<UploadToken>> Initiate(Guid objectGUID, uint formatTypeID, ulong fileSize, bool supportMultipleChunks);
		IServiceCallState<IServiceResult_Upload<ScalarResult>> Transfer(string uploadID, uint chunkIndex, FileData fileData);
	}
}