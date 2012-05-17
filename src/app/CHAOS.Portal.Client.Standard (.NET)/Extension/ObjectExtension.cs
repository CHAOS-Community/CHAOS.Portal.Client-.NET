using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;
using Object = CHAOS.Portal.Client.Data.MCM.Object;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class ObjectExtension : AExtension, IObjectExtension
	{
		public ObjectExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }

		public IServiceCallState<IServiceResult_MCM<Object>> Get(string query, string sort, bool includeMetadata, bool includeFiles, bool includeObjectRelations, int pageIndex, int pageSize)
		{
			return CallService<IServiceResult_MCM<Object>>(HTTPMethod.GET, query, sort, includeMetadata, includeFiles, includeObjectRelations, pageIndex, pageSize);
		}

		public IServiceCallState<IServiceResult_MCM<Object>> Create(Guid? GUID, uint objectTypeID, uint folderID)
		{
			return CallService<IServiceResult_MCM<Object>>(HTTPMethod.GET, GUID, objectTypeID, folderID);
		}
	}
}