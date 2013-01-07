using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public class MetadataExtension : AExtension, IMetadataExtension
	{
		public IServiceCallState<Metadata> Set(Guid objectGUID, Guid metadataSchemaGUID, string languageCode, uint? revisionID, XElement metadataXML)
		{
			return CallService<Metadata>(HTTPMethod.POST, objectGUID, metadataSchemaGUID, languageCode, revisionID, metadataXML);
		}
	}
}