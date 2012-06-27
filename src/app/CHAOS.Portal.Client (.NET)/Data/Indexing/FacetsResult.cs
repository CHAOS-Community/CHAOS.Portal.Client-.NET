using System.Collections.Generic;

namespace CHAOS.Portal.Client.Data.Indexing
{
	public class FacetsResult
	{
		public string Value { get; set; }
		public IList<Facet> Facets { get; set; }
 
	}
}