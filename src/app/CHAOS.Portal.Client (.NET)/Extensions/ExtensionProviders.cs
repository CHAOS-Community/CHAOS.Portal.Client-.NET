namespace CHAOS.Portal.Client.Extensions
{
	public static class ExtensionProviders
	{
		 public static ISessionExtension Session(this IPortalClient portalClient)
		 {
			 return ExtensionProviderService.GetExtension<SessionExtension>(portalClient);
		 }

		 public static IEmailPasswordExtension EmailPassword(this IPortalClient portalClient)
		 {
			 return ExtensionProviderService.GetExtension<EmailPasswordExtension>(portalClient);
		 }

		 public static IClientSettingsExtension ClientSettings(this IPortalClient portalClient)
		 {
			 return ExtensionProviderService.GetExtension<ClientSettingsExtension>(portalClient);
		 }
	}
}