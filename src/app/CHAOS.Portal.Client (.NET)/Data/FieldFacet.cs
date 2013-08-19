using System.Collections.Generic;

namespace CHAOS.Portal.Client.Data
{
	public class FieldFacet
	{
		public string Value { get; set; }
		public IList<Facet> Facets {get; set;}
	}
}