using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IObjectRelationExtension
	{
		IServiceCallState<ScalarResult> Create(Guid object1Guid, Guid object2Guid, uint objectRelationTypeID, Guid? metadataSchemaGuid = null, string languageCode = null, XElement metadataXml = null, uint? revisionID = null, int? sequence = null);
		IServiceCallState<ScalarResult> Delete(Guid object1Guid, Guid object2Guid, uint objectRelationTypeID);
	}
}