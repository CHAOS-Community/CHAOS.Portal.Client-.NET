using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class ObjectExtension : Extension, IObjectExtension
	{
		public ObjectExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }

		public IServiceCallState<IServiceResult_MCM<Object>> Get(string query, string sort, bool includeMetadata, bool includeFiles, bool includeObjectRelations, int pageIndex, int pageSize)
		{
			return CallService<IServiceResult_MCM<Object>>(HTTPMethod.GET, query, sort, includeMetadata, includeFiles, includeObjectRelations, pageIndex, pageSize);
		}
	}
}