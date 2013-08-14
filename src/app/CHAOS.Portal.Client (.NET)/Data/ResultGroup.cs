using System.Collections.Generic;

namespace CHAOS.Portal.Client.Data
{
	public class ResultGroup<T>
	{
		public uint FoundCount { get; set; }
		public uint StartIndex { get; set; }
		public string Value { get; set; }
		public IList<T> Results { get; set; }
	}
}