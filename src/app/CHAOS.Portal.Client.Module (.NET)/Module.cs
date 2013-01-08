using CHAOS.Portal.Client.Managers;
using CHAOS.Portal.Client.Standard;
using CHAOS.Portal.Client.Standard.Managers;
using CHAOS.Portal.Client.Standard.ServiceCall;
using Ninject.Modules;

namespace CHAOS.Portal.Client.Module
{
	public class Module : NinjectModule
	{
		public override void Load()
		{
			Bind(typeof (IServiceCall<>)).To(typeof(ServiceCall<>));
			Bind<IServiceCallFactory>().To<ServiceCallFactory>().InSingletonScope();
			Bind<IPortalClient>().To<PortalClient>().InSingletonScope();
			
			Bind<IFolderManager>().To<FolderManager>().InSingletonScope();
			Bind<IObjectManager>().To<ObjectManager>().InSingletonScope();
			Bind<IMetadataSchemaManager>().To<MetadataSchemaManager>().InSingletonScope();
			Bind<IMCMTypesManager>().To<MCMTypesManager>().InSingletonScope();
			Bind<ILanguageManager>().To<LanguageManager>().InSingletonScope();
			Bind<IUserManager>().To<UserManager>().InSingletonScope();
			Bind<IFileUploader>().To<FileUploader>();
			Bind<IFileUploadManager>().To<FileUploadManager>().InSingletonScope();
		}
	}
}