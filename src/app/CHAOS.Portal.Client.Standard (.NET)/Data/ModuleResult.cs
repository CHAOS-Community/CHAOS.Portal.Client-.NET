using System;
using System.Collections.Generic;
using CHAOS.Portal.Client.Data;

namespace CHAOS.Portal.Client.Standard.Data
{
	public abstract class ModuleResult : IModuleResult
	{
		public uint Count { get; set; }

		public uint TotalCount { get; set; }

		public Exception Error { get; set; }

		public abstract void Add(object data);
	}

	public class ModuleResult<T> : ModuleResult, IModuleResult<T>
	{
		private readonly IList<T> _data;

		public IList<T> Data { get { return _data; } }

		public ModuleResult()
		{
			_data = new List<T>();
		}

		public override void Add(object data)
		{
			_data.Add((T) data);
		}
	}
}