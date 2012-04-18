using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Linq;
using CHAOS.Common.Utilities;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Standard.Data;
using CHAOS.Serialization.XML;
using System.Linq;
using CHAOS.Common.Extensions;

namespace CHAOS.Portal.Client.Standard.ServiceCall
{
	public class ResultParser<T> where T : class, IServiceResult
	{
		private readonly T _Result;
		private readonly IXMLSerializer _Serializer;

		private readonly IDictionary<string, PropertyInfo> _Properties;

		private static IDictionary<Type, IDictionary<string, PropertyInfo>> _MappedModules;

		public ResultParser(T result, IXMLSerializer serializer)
		{
			_Result = ArgumentUtilities.ValidateIsNotNull("result", result);
			_Serializer = ArgumentUtilities.ValidateIsNotNull("serializer", serializer);

			_Properties = GetMappedModules(result.GetType());
		}

		private static IDictionary<string, PropertyInfo> GetMappedModules(Type type)
		{
			if(_MappedModules == null)
				_MappedModules = new Dictionary<Type, IDictionary<string, PropertyInfo>>();

			if(!_MappedModules.ContainsKey(type))
			{
				var properties = new Dictionary<string, PropertyInfo>();

				foreach (var property in type.GetProperties())
				{
					var attribute = (ModuleAttribute) property.GetCustomAttributes(typeof(ModuleAttribute), false).SingleOrDefault();

					if(attribute == null)
						continue;

					properties.Add(attribute.Name, property);
				}

				_MappedModules.Add(type, properties);

				return properties;
			}

			return _MappedModules[type];
		}

		public T Parse(string data)
		{
			var xmlData = XDocument.Parse(ArgumentUtilities.ValidateIsNotNullOrEmpty("data", data)).Root;

			_Result.Duration = new TimeSpan(0, 0, 0, 0, (int)long.Parse(xmlData.Attribute("Duration").Value));
			var error = xmlData.Element("Error");
			
			if(error != null)
				throw new Exception(string.Format("Server returned an error: {0}", error.Element("Message").Value));

			foreach (var property in _Properties)
			{
				var moduleXML = xmlData.Descendants("ModuleResult").FirstOrDefault(element => element.Attribute("Fullname").Value == property.Key);

				if(moduleXML == null)
					continue;

				var moduleResult = (ModuleResult)property.Value.GetValue(_Result, null);

				moduleResult.Count = moduleXML.Attribute("Count").DoIfIsNotNull(att => uint.Parse(att.Value));
				moduleResult.TotalCount = moduleXML.Attribute("TotalCount").DoIfIsNotNull(att => uint.Parse(att.Value));

				var moduleDataType = moduleResult.GetType().GetGenericArguments()[0];

				var moduleError = moduleXML.Element("Results").Element("Error");

				if(!moduleError.IsNull())
				{
					moduleResult.Error = new Exception(moduleError.Element("Message").Value);
					
					continue;
				}

				foreach (var element in moduleXML.Element("Results").Elements("Result"))
					moduleResult.Add(_Serializer.Deserialize(moduleDataType, XDocument.Parse(element.ToString(SaveOptions.DisableFormatting)), false));
			}
			return _Result;
		}
	}
}