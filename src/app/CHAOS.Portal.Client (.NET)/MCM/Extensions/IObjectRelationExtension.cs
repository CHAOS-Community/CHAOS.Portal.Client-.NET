using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IObjectRelationExtension
	{
		IServiceCallState<ScalarResult> Create(Guid object1GUID, Guid object2GUID, uint objectRelationTypeID, Guid? metadataSchemaGUID = null, string languageCode = null, XElement metadataXML = null, uint? revisionID = null, int? sequence = null);
		IServiceCallState<ScalarResult> Delete(Guid object1GUID, Guid object2GUID, uint objectRelationTypeID);
	}
}