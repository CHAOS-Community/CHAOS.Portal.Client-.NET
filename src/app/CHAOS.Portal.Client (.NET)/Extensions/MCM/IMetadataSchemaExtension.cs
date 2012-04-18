using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions.MCM
{
	public interface IMetadataSchemaExtension
	{
		IServiceCallState<IServiceResult_MCM<MetadataSchema>> Get(int? id);
	}
}