using System;
using System.Collections.Generic;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using Object = CHAOS.Portal.Client.MCM.Data.Object;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public class ObjectExtension : AExtension, IObjectExtension
	{
		public IServiceCallState<PagedResult<Object>> Get(IEnumerable<Guid> objectGuids, uint? folderId, Guid? accessPointGuid, bool includeAccessPoints, bool includeMetadata, bool includeFiles, bool includeObjectRelations, bool includeFolders, uint pageSize, uint pageIndex)
		{
			return CallService<PagedResult<Object>>(HTTPMethod.GET, objectGuids, folderId, accessPointGuid, includeAccessPoints, includeMetadata, includeFiles, includeObjectRelations, includeFolders, pageSize, pageIndex);
		}

		public IServiceCallState<PagedResult<Object>> Create(Guid? guid, uint objectTypeID, uint folderID)
		{
			return CallService<PagedResult<Object>>(HTTPMethod.GET, guid, objectTypeID, folderID);
		}

		public IServiceCallState<PagedResult<ScalarResult>> Delete(Guid? guid)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.GET, guid);
		}

		public IServiceCallState<PagedResult<ScalarResult>> SetPublishSettings(Guid objectGUID, Guid accessPointGUID, DateTime? startDate, DateTime? endDate)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.GET, objectGUID, accessPointGUID, startDate, endDate);
		}
	}
}