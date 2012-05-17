using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;
using Object = CHAOS.Portal.Client.Data.MCM.Object;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IObjectExtension
	{
		IServiceCallState<IServiceResult_MCM<Object>> Get(string query, string sort, bool includeMetadata, bool includeFiles, bool includeObjectRelations, int pageIndex, int pageSize);
		IServiceCallState<IServiceResult_MCM<Object>> Create(Guid? GUID, uint objectTypeID, uint folderID);
	}
}