using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public class LanguageExtension : AExtension, ILanguageExtension
	{
		public IServiceCallState<Language> Get(string name, string languageCode)
		{
			return CallService<Language>(HTTPMethod.GET, name, languageCode);
		}

		public IServiceCallState<Language> Create(string name, string languageCode)
		{
			return CallService<Language>(HTTPMethod.GET, name, languageCode);
		}

		public IServiceCallState<ScalarResult> Update(string languageCode, string newName)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, languageCode, newName);
		}

		public IServiceCallState<ScalarResult> Delete(string languageCode)
		{
			return CallService<ScalarResult>(HTTPMethod.GET, languageCode);
		}
	}
}