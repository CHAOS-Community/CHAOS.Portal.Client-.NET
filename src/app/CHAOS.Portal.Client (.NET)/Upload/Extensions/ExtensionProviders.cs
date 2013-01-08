using CHAOS.Portal.Client.Extensions;

namespace CHAOS.Portal.Client.Upload.Extensions
{
	public static class ExtensionProviders
	{
		public static IUploadExtension Upload(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<UploadExtension>(portalClient);
		}
	}
}