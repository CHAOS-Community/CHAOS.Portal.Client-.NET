using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IMetadataSchemaExtension
	{
		IServiceCallState<IServiceResult_MCM<MetadataSchema>> Get(Guid? guid);
	}
}