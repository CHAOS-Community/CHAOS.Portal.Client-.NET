using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
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

		public IServiceCallState<MetadataSchema> Create(string name, XElement schemaXml, Guid? guid = null)
		{
			return CallService<MetadataSchema>(HTTPMethod.POST, name, schemaXml, guid);
		}

		public IServiceCallState<MetadataSchema> Update(string name, XElement schemaXml, Guid guid)
		{
			return CallService<MetadataSchema>(HTTPMethod.POST, name, schemaXml, guid);
		}

		public IServiceCallState<ScalarResult> Delete(Guid guid)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, guid);
		}
	}
}