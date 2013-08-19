using System.Collections.Generic;

namespace CHAOS.Portal.Client.Data
{
	public class QueryResult : IServiceBody
	{
		public IList<FieldFacet> FieldFacets { get; set; }
	}
}