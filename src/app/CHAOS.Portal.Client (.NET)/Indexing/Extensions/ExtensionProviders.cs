using CHAOS.Portal.Client.Extensions;

namespace CHAOS.Portal.Client.Indexing.Extensions
{
	public static class ExtensionProviders
	{
		public static IIndexExtension Index(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<IndexExtension>(portalClient);
		}
	}
}