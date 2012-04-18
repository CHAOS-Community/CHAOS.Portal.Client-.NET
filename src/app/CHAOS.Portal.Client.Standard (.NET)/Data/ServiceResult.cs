using System;
using CHAOS.Portal.Client.Data;

namespace CHAOS.Portal.Client.Standard.Data
{
	public abstract class ServiceResult : IServiceResult
	{
		public TimeSpan Duration { get; set; }
	}

	
	public class ServiceResult_Portal<T> : ServiceResult, IServiceResult_Portal<T>
	{
		[Module("CHAOS.Portal")]
		public IModuleResult<T> Portal { get; private set; }

		public ServiceResult_Portal(IModuleResult<T> moduleResult)
		{
			Portal = moduleResult;
		}
	}

	public class ServiceResult_MCM<T> : ServiceResult, IServiceResult_MCM<T>
	{
		[Module("CHAOS.MCM.Module.Standard.MCMModule")]
		public IModuleResult<T> MCM { get; private set; }

		public ServiceResult_MCM(IModuleResult<T> moduleResult)
		{
			MCM = moduleResult;
		}
	}
}