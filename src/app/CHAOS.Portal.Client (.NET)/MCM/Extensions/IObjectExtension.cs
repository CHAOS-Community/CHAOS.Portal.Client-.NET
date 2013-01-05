using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;
using Object = CHAOS.Portal.Client.MCM.Data.Object;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IObjectExtension
	{
		IServiceCallState<IServiceResult_MCM<Object>> Get(string query, string sort, int pageIndex, int pageSize, bool includeMetadata = false, bool includeFiles = false, bool includeObjectRelations = false, bool includeAccessPoints = false, Guid? accessPointGUID = null);
		IServiceCallState<IServiceResult_MCM<Object>> Create(Guid? GUID, uint objectTypeID, uint folderID);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Delete(Guid? GUID);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> SetPublishSettings(Guid objectGUID, Guid accessPointGUID, DateTime? startDate, DateTime? endDate);
	}
}