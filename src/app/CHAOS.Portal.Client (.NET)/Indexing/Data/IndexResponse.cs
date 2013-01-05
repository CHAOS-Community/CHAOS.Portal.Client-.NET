using System.Collections.Generic;

namespace CHAOS.Portal.Client.Indexing.Data
{
	public class IndexResponse
	{
		public IList<FacetsResult> FacetQueriesResult { get; set; }
		public IList<FacetsResult> FacetFieldsResult { get; set; }
	}
}