using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Utilities;
using CHAOS.Serialization.XML;

namespace CHAOS.Portal.Client.Standard.ServiceCall
{
	public class ResultParser<T> where T : class, IServiceBody
	{
		private readonly IXMLSerializer _serializer;

		public ResultParser(IXMLSerializer serializer)
		{
			_serializer = ArgumentUtilities.ValidateIsNotNull("serializer", serializer);
		}

		public ServiceResponse<T> Parse(string data)
		{
			var xmlData = XDocument.Parse(ArgumentUtilities.ValidateIsNotNullOrEmpty("data", data));

			try
			{
				return _serializer.Deserialize<ServiceResponse<T>>(xmlData, false);
			}
			catch (Exception e)
			{
				return new ServiceResponse<T> { Error = new Exception("Failed to parse service response", e) };
			}
		}
	}
}