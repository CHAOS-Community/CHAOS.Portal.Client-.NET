using System;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public class MetadataSchemaExtension : AExtension, IMetadataSchemaExtension
	{
		public IServiceCallState<MetadataSchema> Get(Guid? guid)
		{
			return CallService<MetadataSchema>(HTTPMethod.GET, guid);
		}
	}
}