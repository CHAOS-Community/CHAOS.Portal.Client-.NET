using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public class MetadataExtension : AExtension, IMetadataExtension
	{
		public IServiceCallState<PagedResult<Metadata>> Set(Guid objectGuid, Guid metadataSchemaGuid, string languageCode, uint? revisionID, XElement metadataXml)
		{
			return CallService<PagedResult<Metadata>>(HTTPMethod.POST, objectGuid, metadataSchemaGuid, languageCode, revisionID, metadataXml);
		}
	}
}