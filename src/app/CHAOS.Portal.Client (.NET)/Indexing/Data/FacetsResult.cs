using System.Collections.Generic;

namespace CHAOS.Portal.Client.Indexing.Data
{
	public class FacetsResult
	{
		public string Value { get; set; }
		public IList<Facet> Facets { get; set; }
	}
}