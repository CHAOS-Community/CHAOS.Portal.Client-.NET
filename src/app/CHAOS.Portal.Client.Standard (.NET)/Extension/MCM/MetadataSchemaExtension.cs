using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.Extensions.MCM;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension.MCM
{
	public class MetadataSchemaExtension : Extension, IMetadataSchemaExtension
	{
		public MetadataSchemaExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }

		public IServiceCallState<IServiceResult_MCM<MetadataSchema>> Get(int? id)
		{
			return CallService<IServiceResult_MCM<MetadataSchema>>(HTTPMethod.GET, id);
		}
	}
}