using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Upload.Data;
using CHAOS.Web.Data;

namespace CHAOS.Portal.Client.Upload.Extensions
{
	public class UploadExtension : AExtension, IUploadExtension
	{
		public IServiceCallState<PagedResult<UploadToken>> Initiate(Guid objectGUID, uint formatTypeID, ulong fileSize, bool supportMultipleChunks)
		{
			return CallService<PagedResult<UploadToken>>(HTTPMethod.GET, objectGUID, formatTypeID, fileSize, supportMultipleChunks);
		}

		public IServiceCallState<PagedResult<ScalarResult>> Transfer(string uploadID, uint chunkIndex, FileData fileData)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.POST, uploadID, chunkIndex, new FileMultipartElement(fileData.Name, fileData.Data, (int) fileData.Length));
		}
	}
}