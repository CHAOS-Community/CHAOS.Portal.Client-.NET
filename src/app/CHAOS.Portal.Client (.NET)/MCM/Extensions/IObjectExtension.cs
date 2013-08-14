using System;
using System.Collections.Generic;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;
using Object = CHAOS.Portal.Client.MCM.Data.Object;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IObjectExtension
	{
		IServiceCallState<PagedResult<Object>> Get(IEnumerable<Guid> objectGuids, bool includeAccessPoints = false, bool includeMetadata = false, bool includeFiles = false, bool includeObjectRelations = false, bool includeFolders = false);
		IServiceCallState<PagedResult<Object>> Create(Guid? guid, uint objectTypeID, uint folderID);
		IServiceCallState<PagedResult<ScalarResult>> Delete(Guid? guid);
		IServiceCallState<PagedResult<ScalarResult>> SetPublishSettings(Guid objectGUID, Guid accessPointGUID, DateTime? startDate, DateTime? endDate);
	}
}