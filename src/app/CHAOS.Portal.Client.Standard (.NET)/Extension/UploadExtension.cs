using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class UploadExtension : AExtension, IUploadExtension
	{
		public UploadExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }

		public IServiceCallState<IServiceResult_Portal<UploadToken>> Initiate(Guid objectGUID, uint formatID, ulong fileSize, bool supportMultipleChunks)
		{
			return CallService<IServiceResult_Portal<UploadToken>>(HTTPMethod.GET, objectGUID, formatID, fileSize, supportMultipleChunks);
		}

		public IServiceCallState<IServiceResult_Portal<ScalarResult>> Transfer(string uploadID, uint chunkIndex, byte[] fileData)
		{
			return CallService<IServiceResult_Portal<ScalarResult>>(HTTPMethod.POST, uploadID, chunkIndex, fileData);
		}
	}
}