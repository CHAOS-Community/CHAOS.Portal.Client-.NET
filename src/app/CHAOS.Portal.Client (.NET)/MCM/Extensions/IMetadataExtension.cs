using System;
using System.Xml.Linq;

using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IMetadataExtension
	{
		IServiceCallState<Metadata> Set(Guid objectGuid, Guid metadataSchemaGuid, string languageCode, uint? revisionID, XElement metadataXml);
	}
}