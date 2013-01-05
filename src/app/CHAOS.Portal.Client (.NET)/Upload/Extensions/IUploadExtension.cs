using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Upload.Data;

namespace CHAOS.Portal.Client.Upload.Extensions
{
	public interface IUploadExtension
	{
		IServiceCallState<UploadToken> Initiate(Guid objectGUID, uint formatTypeID, ulong fileSize, bool supportMultipleChunks);
		IServiceCallState<ScalarResult> Transfer(string uploadID, uint chunkIndex, FileData fileData);
	}
}