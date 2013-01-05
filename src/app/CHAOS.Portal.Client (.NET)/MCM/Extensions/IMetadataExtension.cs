using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IMetadataExtension
	{
		IServiceCallState<IServiceResult_MCM<Metadata>> Set(Guid objectGUID, Guid metadataSchemaGUID, string languageCode, uint? revisionID, XElement metadataXML);
	}
}