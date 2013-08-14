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
		public IServiceCallState<PagedResult<MetadataSchema>> Get(Guid? guid)
		{
			return CallService<PagedResult<MetadataSchema>>(HTTPMethod.GET, guid);
		}

		public IServiceCallState<PagedResult<MetadataSchema>> Create(string name, XElement schemaXml, Guid? guid = null)
		{
			return CallService<PagedResult<MetadataSchema>>(HTTPMethod.POST, name, schemaXml, guid);
		}

		public IServiceCallState<PagedResult<MetadataSchema>> Update(string name, XElement schemaXml, Guid guid)
		{
			return CallService<PagedResult<MetadataSchema>>(HTTPMethod.POST, name, schemaXml, guid);
		}

		public IServiceCallState<PagedResult<ScalarResult>> Delete(Guid guid)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.GET, guid);
		}
	}
}