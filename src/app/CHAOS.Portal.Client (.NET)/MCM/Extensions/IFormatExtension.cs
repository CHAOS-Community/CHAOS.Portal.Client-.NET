using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface IFormatExtension
	{
		IServiceCallState<Format> Get(uint? id, string name);
		IServiceCallState<ScalarResult> Create(uint formatCategoryID, string name, XDocument formatXML, string mimeType, string extension);
	}
}