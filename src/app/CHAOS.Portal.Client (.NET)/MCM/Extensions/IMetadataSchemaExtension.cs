using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IMetadataSchemaExtension
	{
		IServiceCallState<MetadataSchema> Get(Guid? guid = null);
		IServiceCallState<MetadataSchema> Create(string name, XElement schemaXml, Guid? guid = null);
		IServiceCallState<MetadataSchema> Update(string name, XElement schemaXml, Guid guid);
		IServiceCallState<ScalarResult> Delete(Guid guid);
	}
}