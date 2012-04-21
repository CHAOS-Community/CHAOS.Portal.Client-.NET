using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class MetadataSchemaExtension : Extension, IMetadataSchemaExtension
	{
		public MetadataSchemaExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }

		public IServiceCallState<IServiceResult_MCM<MetadataSchema>> Get(Guid? guid)
		{
			return CallService<IServiceResult_MCM<MetadataSchema>>(HTTPMethod.GET, guid);
		}
	}
}