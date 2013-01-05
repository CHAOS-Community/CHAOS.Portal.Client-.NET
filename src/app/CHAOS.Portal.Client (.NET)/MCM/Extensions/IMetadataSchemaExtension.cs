using System;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IMetadataSchemaExtension
	{
		IServiceCallState<MetadataSchema> Get(Guid? guid = null);
	}
}