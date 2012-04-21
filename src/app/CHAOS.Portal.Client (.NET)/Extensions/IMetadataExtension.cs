using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IMetadataExtension
	{
		IServiceCallState<IServiceResult_MCM<Metadata>> Set(Guid objectGUID, Guid metadataSchemaGUID, string languageCode, uint? revisionID, XElement metadataXML);
	}
}