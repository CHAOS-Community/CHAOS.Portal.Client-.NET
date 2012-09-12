using System;
using CHAOS.Portal.Client.Data;

namespace CHAOS.Portal.Client.Standard.Data
{
	public abstract class ServiceResult : IServiceResult
	{
		public TimeSpan Duration { get; set; }
	}

	public class ServiceResult_EmailPassword<T> : ServiceResult, IServiceResult_EmailPassword<T>
	{
		[Module("EmailPassword")]
		public IModuleResult<T> EmailPassword { get; private set; }

		public ServiceResult_EmailPassword(IModuleResult<T> moduleResult)
		{
			EmailPassword = moduleResult;
		}
	}

	public class ServiceResult_SecureCookie<T> : ServiceResult, IServiceResult_SecureCookie<T>
	{
		[Module("SecureCookie")]
		public IModuleResult<T> SecureCookie { get; private set; }

		public ServiceResult_SecureCookie(IModuleResult<T> moduleResult)
		{
			SecureCookie = moduleResult;
		}
	}
	
	public class ServiceResult_Portal<T> : ServiceResult, IServiceResult_Portal<T>
	{
		[Module("Portal")]
		public IModuleResult<T> Portal { get; private set; }

		public ServiceResult_Portal(IModuleResult<T> moduleResult)
		{
			Portal = moduleResult;
		}
	}

	public class ServiceResult_MCM<T> : ServiceResult, IServiceResult_MCM<T>
	{
		[Module("MCM")]
		public IModuleResult<T> MCM { get; private set; }

		public ServiceResult_MCM(IModuleResult<T> moduleResult)
		{
			MCM = moduleResult;
		}
	}

	public class ServiceResult_Statistics<T> : ServiceResult, IServiceResult_Statistics<T>
	{
		[Module("Statistics")]
		public IModuleResult<T> Statistics { get; private set; }

		public ServiceResult_Statistics(IModuleResult<T> moduleResult)
		{
			Statistics = moduleResult;
		}
	}

	public class ServiceResult_Index<T> : ServiceResult, IServiceResult_Index<T>
	{
		[Module("Index")]
		public IModuleResult<T> Index { get; private set; }

		public ServiceResult_Index(IModuleResult<T> moduleResult)
		{
			Index = moduleResult;
		}
	}

	public class ServiceResult_Upload<T> : ServiceResult, IServiceResult_Upload<T>
	{
		[Module("Upload")]
		public IModuleResult<T> Upload { get; private set; }

		public ServiceResult_Upload(IModuleResult<T> moduleResult)
		{
			Upload = moduleResult;
		}
	}
}