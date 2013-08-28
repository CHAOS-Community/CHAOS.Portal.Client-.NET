using CHAOS.Portal.Client.Extensions;

namespace CHAOS.Portal.Client.MCM.Extensions
{
	public static class ExtensionProviders
	{
		public static IFileExtension File(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<FileExtension>(portalClient);
		}

		public static IFolderExtension Folder(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<FolderExtension>(portalClient);
		}

		public static IFolderTypeExtension FolderType(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<FolderTypeExtension>(portalClient);
		}

		public static IFormatExtension Format(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<FormatExtension>(portalClient);
		}

		public static IFormatTypeExtension FormatType(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<FormatTypeExtension>(portalClient);
		}

		public static ILanguageExtension Language(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<LanguageExtension>(portalClient);
		}

		public static ILinkExtension Link(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<LinkExtension>(portalClient);
		}

		public static IMetadataExtension Metadata(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<MetadataExtension>(portalClient);
		}

		public static IMetadataSchemaExtension MetadataSchema(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<MetadataSchemaExtension>(portalClient);
		}

		public static IObjectExtension Object(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<ObjectExtension>(portalClient);
		}

		public static IObjectRelationExtension ObjectRelation(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<ObjectRelationExtension>(portalClient);
		}

		public static IObjectRelationTypeExtension ObjectRelationType(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<ObjectRelationTypeExtension>(portalClient);
		}

		public static IObjectTypeExtension ObjectType(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<ObjectTypeExtension>(portalClient);
		}

		public static IUserProfileExtension UserProfile(this IPortalClient portalClient)
		{
			return ExtensionProviderService.GetExtension<UserProfileExtension>(portalClient);
		}
	}
}