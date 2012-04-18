using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.Extensions.MCM;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension.MCM
{
	public class MetadataExtension : Extension, IMetadataExtension
	{
		public MetadataExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }


		public IServiceCallState<IServiceResult_MCM<Metadata>> Set(Guid objectGUID, int metadataSchemaID, string languageCode, XElement metadataXML)
		{
			return CallService<IServiceResult_MCM<Metadata>>(HTTPMethod.POST, objectGUID, metadataSchemaID, languageCode, metadataXML);
		}
	}
}