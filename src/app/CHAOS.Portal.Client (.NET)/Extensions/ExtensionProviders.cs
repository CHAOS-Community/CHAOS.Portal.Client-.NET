namespace CHAOS.Portal.Client.Extensions
{
	public static class ExtensionProviders
	{
		 public static ISessionExtension Session(this IPortalClient portalClient)
		 {
			 return ExtensionProviderService.GetExtension<SessionExtension>(portalClient);
		 }
	}
}