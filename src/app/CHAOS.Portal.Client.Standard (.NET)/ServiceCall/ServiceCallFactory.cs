using System.Collections.Generic;
using CHAOS.Portal.Client.Data;
using CHAOS.Serialization.Standard.String;
using CHAOS.Serialization.Standard.XML;
using CHAOS.Serialization.String;
using CHAOS.Serialization.XML;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.ServiceCall
{
	public class ServiceCallFactory : IServiceCallFactory
	{
		private readonly IStringSerializer _stringSerializer;
		private readonly IXMLSerializer _xmlSerializer;

		public ServiceCallFactory()
		{
			_stringSerializer = new StringSerializer();
			_xmlSerializer = new XMLSerializer(_stringSerializer);
			_xmlSerializer.Map(typeof(IList<>), typeof(List<>));
		}

		public IServiceCall<T> GetServiceCall<T>() where T : class, IServiceResult
		{
			return new ServiceCall<T>(new ServiceCallState<T>(), new ResultParser<T>(_xmlSerializer), new SmartHTTPRequest(_stringSerializer));
		}
	}
}