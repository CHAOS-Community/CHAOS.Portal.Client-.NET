using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Upload.Data;

namespace CHAOS.Portal.Client.Upload.Extensions
{
	public interface IUploadExtension
	{
		IServiceCallState<PagedResult<UploadToken>> Initiate(Guid objectGUID, uint formatTypeID, ulong fileSize, bool supportMultipleChunks);
		IServiceCallState<PagedResult<ScalarResult>> Transfer(string uploadID, uint chunkIndex, FileData fileData);
	}
}