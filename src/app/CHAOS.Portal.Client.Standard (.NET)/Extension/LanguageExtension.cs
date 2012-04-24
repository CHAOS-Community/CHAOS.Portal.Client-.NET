using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
	public class LanguageExtension : AExtension, ILanguageExtension
	{
		public LanguageExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }

		public IServiceCallState<IServiceResult_MCM<Language>> Get(string name, string languageCode)
		{
			return CallService<IServiceResult_MCM<Language>>(HTTPMethod.GET, name, languageCode);
		}

		public IServiceCallState<IServiceResult_MCM<Language>> Create(string name, string languageCode)
		{
			return CallService<IServiceResult_MCM<Language>>(HTTPMethod.GET, name, languageCode);
		}

		public IServiceCallState<IServiceResult_MCM<ScalarResult>> Update(string languageCode, string newName)
		{
			return CallService<IServiceResult_MCM<ScalarResult>>(HTTPMethod.GET, languageCode, newName);
		}

		public IServiceCallState<IServiceResult_MCM<ScalarResult>> Delete(string languageCode)
		{
			return CallService<IServiceResult_MCM<ScalarResult>>(HTTPMethod.GET, languageCode);
		}
	}
}