using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public interface ILanguageExtension
	{
		IServiceCallState<PagedResult<Language>> Get(string name = null, string languageCode = null);
		IServiceCallState<PagedResult<Language>> Create(string name, string languageCode);
		IServiceCallState<PagedResult<ScalarResult>> Update(string languageCode, string newName);
		IServiceCallState<PagedResult<ScalarResult>> Delete(string languageCode);
	}
}