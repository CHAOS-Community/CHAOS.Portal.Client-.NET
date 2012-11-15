using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface ILanguageExtension
	{
		IServiceCallState<IServiceResult_MCM<Language>> Get(string name = null, string languageCode = null);
		IServiceCallState<IServiceResult_MCM<Language>> Create(string name, string languageCode);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Update(string languageCode, string newName);
		IServiceCallState<IServiceResult_MCM<ScalarResult>> Delete(string languageCode);
	}
}