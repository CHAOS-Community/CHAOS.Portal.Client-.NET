using System.Collections.Generic;
using CHAOS.Serialization.Standard.String;
using CHAOS.Serialization.Standard.XML;
using CHAOS.Serialization.String;
using CHAOS.Serialization.XML;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.ServiceCall
{
	public class ServiceCallFactory
	{
		private readonly IStringSerializer _stringSerializer;
		private readonly IXMLSerializer _xmlSerializer;

		public ServiceCallFactory()
		{
			_stringSerializer = new StringSerializer();
			_xmlSerializer = new XMLSerializer(_stringSerializer);
			_xmlSerializer.Map(typeof(IList<>), typeof(List<>));
		}

		public ServiceCall<T> GetServiceCall<T>() where T : class
		{
			return new ServiceCall<T>(new ServiceCallState<T>(), new ResultParser<T>(_xmlSerializer), new SmartHTTPRequest(_stringSerializer));
		}
	}
}