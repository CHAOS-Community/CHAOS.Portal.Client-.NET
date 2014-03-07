namespace CHAOS.Portal.Client.Extensions
{
	public static class ExtensionProviders
	{
		public static IClientSettingsExtension ClientSettings(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<ClientSettingsExtension>(portalClient);
		}

		public static IEmailPasswordExtension EmailPassword(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<EmailPasswordExtension>(portalClient);
		}

		public static IOAuthExtension OAuth(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<OAuthExtension>(portalClient);
		}

		public static IGroupExtension Group(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<GroupExtension>(portalClient);
		}

		public static ISecureCookieExtension SecureCookie(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<SecureCookieExtension>(portalClient);
		}

		public static ISessionExtension Session(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<SessionExtension>(portalClient);
		}

		public static ISubscriptionExtension Subscription(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<SubscriptionExtension>(portalClient);
		}

		public static IUserExtension User(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<UserExtension>(portalClient);
		}

		public static IUserSettingsExtension UserSettings(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<UserSettingsExtension>(portalClient);
		}

		public static IViewExtension View(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<ViewExtension>(portalClient);
		}
	}
}