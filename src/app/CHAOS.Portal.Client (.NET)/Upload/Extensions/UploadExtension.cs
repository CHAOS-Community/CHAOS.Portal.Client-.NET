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
		public IServiceCallState<UploadToken> Initiate(Guid objectGUID, uint formatTypeID, ulong fileSize, bool supportMultipleChunks)
		{
			return CallService<UploadToken>(HTTPMethod.GET, objectGUID, formatTypeID, fileSize, supportMultipleChunks);
		}

		public IServiceCallState<ScalarResult> Transfer(string uploadID, uint chunkIndex, FileData fileData)
		{
			return CallService<ScalarResult>(HTTPMethod.POST, uploadID, chunkIndex, new FileMultipartElement(fileData.Name, fileData.Data, (int) fileData.Length));
		}
	}
}