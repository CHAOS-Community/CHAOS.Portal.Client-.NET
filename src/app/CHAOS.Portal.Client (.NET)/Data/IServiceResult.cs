using System;

namespace CHAOS.Portal.Client.Data
{
	/// <summary>
	/// Represents the result from a service call.
	/// </summary>
	public interface IServiceResult
	{
		/// <summary>
		/// The time it took the service to process the service call. This does not include latency for the http request.
		/// </summary>
		TimeSpan Duration { get; set; }
	}

	public interface IServiceResult_Portal<TPortal> : IServiceResult
	{
		IModuleResult<TPortal> Portal { get; }
	}

	public interface IServiceResult_MCM<TMCM> : IServiceResult
	{
		IModuleResult<TMCM> MCM { get; }
	}

	public interface IServiceResult_Statistics<TStatistics> : IServiceResult
	{
		IModuleResult<TStatistics> Statistics { get; }
	}

	public interface IServiceResult_Octopus<TOctopus> : IServiceResult
	{
		IModuleResult<TOctopus> Octopus { get; }
	}

	public interface IServiceResult_GeoLocator<TGeoLocator> : IServiceResult
	{
		IModuleResult<TGeoLocator> GeoLocator { get; }
	}

	public interface IServiceResult_MCM_Octopus<TMCM, TOctopus> : IServiceResult
	{
		IModuleResult<TMCM> MCM { get; }
		IModuleResult<TOctopus> Octopus { get; }
	}
}