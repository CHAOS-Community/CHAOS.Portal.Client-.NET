using System;
using System.Collections.Generic;

namespace CHAOS.Portal.Client.Data
{
	/// <summary>
	/// Represents the result from a Portal module.
	/// </summary>
	public interface IModuleResult
	{
		uint Count { set; get; }
		uint TotalCount { set; get; }

		Exception Error { get; set; }
	}
	
	public interface IModuleResult<T> : IModuleResult
	{
		IList<T> Data { get; }
	}
}