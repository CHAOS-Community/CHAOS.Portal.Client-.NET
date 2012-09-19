using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Upload;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;
using CHAOS.Web.Data;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class UploadExtension : AExtension, IUploadExtension
	{
		public UploadExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }

		public IServiceCallState<IServiceResult_Upload<UploadToken>> Initiate(Guid objectGUID, uint formatTypeID, ulong fileSize, bool supportMultipleChunks)
		{
			return CallService<IServiceResult_Upload<UploadToken>>(HTTPMethod.GET, objectGUID, formatTypeID, fileSize, supportMultipleChunks);
		}

		public IServiceCallState<IServiceResult_Upload<ScalarResult>> Transfer(string uploadID, uint chunkIndex, FileData fileData)
		{
			return CallService<IServiceResult_Upload<ScalarResult>>(HTTPMethod.POST, uploadID, chunkIndex, new FileMultipartElement(fileData.Name, fileData.Data, (int) fileData.Length));
		}
	}
}