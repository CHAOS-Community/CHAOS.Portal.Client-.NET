using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public interface ILanguageExtension
	{
		IServiceCallState<Language> Get(string name = null, string languageCode = null);
		IServiceCallState<Language> Create(string name, string languageCode);
		IServiceCallState<ScalarResult> Update(string languageCode, string newName);
		IServiceCallState<ScalarResult> Delete(string languageCode);
	}
}