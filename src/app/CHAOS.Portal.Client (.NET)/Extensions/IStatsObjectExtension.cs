using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface IStatsObjectExtension
	{
		IServiceCallState<IServiceResult_Statistics<ScalarResult>> Set(string repositoryIdentifier, string objectIdentifier, int objectTypeID, int objectCollectionID,
		                       string channelIdentifier, int channelTypeID, int eventTypeID, string objectTitle, string IP,
		                       string city, string country, int userSessionID);
	}
}