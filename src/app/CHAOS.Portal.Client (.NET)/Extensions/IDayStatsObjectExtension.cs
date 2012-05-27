using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.Statistics;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IDayStatsObjectExtension
	{
		IServiceCallState<IServiceResult_Statistics<DayStatsObject>> Get(uint objectCollectionID, string channelIDList, string objectTypeIDList, string eventTypeIDList, DateTime fromDate, DateTime toDate, uint? pageIndex = null, uint? pageSize = null, string sortDirection = null);
	}
}