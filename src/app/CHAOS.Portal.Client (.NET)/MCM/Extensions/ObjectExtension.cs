using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using Object = CHAOS.Portal.Client.MCM.Data.Object;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public class ObjectExtension : AExtension, IObjectExtension
	{
		public IServiceCallState<Object> Get(string query, string sort, int pageIndex, int pageSize, bool includeMetadata, bool includeFiles, bool includeObjectRelations, bool includeAccessPoints, Guid? accessPointGUID)
		{
			return CallService<Object>(HTTPMethod.GET, query, sort, pageIndex, pageSize, includeMetadata, includeFiles, includeObjectRelations, includeAccessPoints, accessPointGUID);
		}

		public IServiceCallState<Object> Create(Guid? guid, uint objectTypeID, uint folderID)
		{
			return CallService<Object>(HTTPMethod.GET, guid, objectTypeID, folderID);
		}

		public IServiceCallState<ScalarResult> Delete(Guid? guid)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, guid);
		}

		public IServiceCallState<ScalarResult> SetPublishSettings(Guid objectGUID, Guid accessPointGUID, DateTime? startDate, DateTime? endDate)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, objectGUID, accessPointGUID, startDate, endDate);
		}
	}
}