using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class StatsObjectExtension : AExtension, IStatsObjectExtension
	{
		public StatsObjectExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }


		public IServiceCallState<IServiceResult_Statistics<ScalarResult>> Set(string repositoryIdentifier, string objectIdentifier, int objectTypeID, int objectCollectionID, string channelIdentifier, int channelTypeID, int eventTypeID, string objectTitle, string IP, string city, string country, int userSessionID)
		{
			return CallService<IServiceResult_Statistics<ScalarResult>>(HTTPMethod.GET, repositoryIdentifier, objectIdentifier, objectTypeID, objectCollectionID, channelIdentifier, channelTypeID, eventTypeID, objectTitle, IP, city, country, userSessionID);
		}
	}
}