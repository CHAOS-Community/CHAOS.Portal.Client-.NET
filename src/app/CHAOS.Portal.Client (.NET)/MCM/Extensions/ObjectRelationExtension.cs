using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public class ObjectRelationExtension : AExtension, IObjectRelationExtension
	{
		public IServiceCallState<ScalarResult> Create(Guid object1GUID, Guid object2GUID, uint objectRelationTypeID, Guid? metadataSchemaGUID = null, string languageCode = null, XElement metadataXML = null, uint? revisionID = null, int? sequence = null)
		{
			return CallService<ScalarResult>(HTTPMethod.POST, object1GUID, object2GUID, objectRelationTypeID, metadataSchemaGUID, languageCode, metadataXML, revisionID, sequence);
		}

		public IServiceCallState<ScalarResult> Delete(Guid object1GUID, Guid object2GUID, uint objectRelationTypeID)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, object1GUID, object2GUID, objectRelationTypeID);
		}
	}
}