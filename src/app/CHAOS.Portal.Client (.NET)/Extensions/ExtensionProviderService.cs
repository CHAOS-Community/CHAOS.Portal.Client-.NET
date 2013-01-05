using System;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
	public static class ExtensionProviderService
	{
		 public static T GetExtension<T>(IPortalClient portalClient) where T : IExtension
		 {
			 var extension = Activator.CreateInstance<T>(); //TODO: Cache extension
			 extension.Initialize((IServiceCaller) portalClient);

			 return extension;
		 }
	}
}