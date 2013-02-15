using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public class ObjectRelationExtension : AExtension, IObjectRelationExtension
	{
		public IServiceCallState<ScalarResult> Create(Guid object1Guid, Guid object2Guid, uint objectRelationTypeID, Guid? metadataSchemaGuid = null, string languageCode = null, XElement metadataXml = null, uint? revisionID = null, int? sequence = null)
		{
			return CallService<ScalarResult>(HTTPMethod.POST, object1Guid, object2Guid, objectRelationTypeID, metadataSchemaGuid, languageCode, metadataXml, revisionID, sequence);
		}

		public IServiceCallState<ScalarResult> Delete(Guid object1Guid, Guid object2Guid, uint objectRelationTypeID)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, object1Guid, object2Guid, objectRelationTypeID);
		}
	}
}