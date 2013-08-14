using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public class FormatExtension : AExtension, IFormatExtension
	{
		public IServiceCallState<PagedResult<Format>> Get(uint? id, string name)
		{
			return CallService<PagedResult<Format>>(HTTPMethod.GET, id, name);
		}

		public IServiceCallState<PagedResult<ScalarResult>> Create(uint formatCategoryID, string name, XDocument formatXML, string mimeType, string extension)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.POST, formatCategoryID, name, formatXML, mimeType, extension);
		}
	}
}