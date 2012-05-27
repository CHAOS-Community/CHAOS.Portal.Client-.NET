using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Statistics;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class DayStatsObjectExtension : AExtension, IDayStatsObjectExtension
	{
		public DayStatsObjectExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }
		
		public IServiceCallState<IServiceResult_Statistics<DayStatsObject>> Get(uint objectCollectionID, string channelIDList, string objectTypeIDList, string eventTypeIDList, DateTime fromDate, DateTime toDate, uint? pageIndex, uint? pageSize, string sortDirection)
		{
			return CallService<IServiceResult_Statistics<DayStatsObject>>(HTTPMethod.GET, objectCollectionID, channelIDList, objectTypeIDList, eventTypeIDList, fromDate, toDate, pageIndex, pageSize, sortDirection);
		}
	}
}