using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public class LanguageExtension : AExtension, ILanguageExtension
	{
		public IServiceCallState<PagedResult<Language>> Get(string name, string languageCode)
		{
			return CallService<PagedResult<Language>>(HTTPMethod.GET, name, languageCode);
		}

		public IServiceCallState<PagedResult<Language>> Create(string name, string languageCode)
		{
			return CallService<PagedResult<Language>>(HTTPMethod.GET, name, languageCode);
		}

		public IServiceCallState<PagedResult<ScalarResult>> Update(string languageCode, string newName)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.GET, languageCode, newName);
		}

		public IServiceCallState<PagedResult<ScalarResult>> Delete(string languageCode)
		{
			return CallService<PagedResult<ScalarResult>>(HTTPMethod.GET, languageCode);
		}
	}
}