using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.GeoLocator;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class LocationExtension : AExtension, ILocationExtension
	{
		public LocationExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }

		public IServiceCallState<IServiceResult_GeoLocator<LocationInfo>> Get(string ip)
		{
			throw new NotImplementedException();
		}
	}
}