using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class UploadExtension : AExtension, IUploadExtension
	{
		public UploadExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }

		public IServiceCallState<IServiceResult_Portal<ScalarResult>> Initiate(Guid objectGUID, uint formatID, uint chunkSize, uint noOfChunks)
		{
			return CallService<IServiceResult_Portal<ScalarResult>>(HTTPMethod.GET, objectGUID, formatID, chunkSize, noOfChunks);
		}
	}
}