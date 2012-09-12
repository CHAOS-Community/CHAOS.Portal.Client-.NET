using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Managers;
using CHAOS.Portal.Client.Standard;
using CHAOS.Portal.Client.Standard.Data;
using CHAOS.Portal.Client.Standard.Managers;
using CHAOS.Portal.Client.Standard.ServiceCall;
using Ninject.Modules;

namespace CHAOS.Portal.Client.Module
{
	public class Module : NinjectModule
	{
		public override void Load()
		{
			Bind(typeof (IModuleResult<>)).To(typeof (ModuleResult<>));
			Bind(typeof (IServiceResult)).To(typeof (ServiceResult));
			Bind(typeof (IServiceResult_EmailPassword<>)).To(typeof (ServiceResult_EmailPassword<>));
			Bind(typeof (IServiceResult_SecureCookie<>)).To(typeof (ServiceResult_SecureCookie<>));
			Bind(typeof (IServiceResult_Portal<>)).To(typeof (ServiceResult_Portal<>));
			Bind(typeof (IServiceResult_MCM<>)).To(typeof (ServiceResult_MCM<>));
			Bind(typeof (IServiceResult_Statistics<>)).To(typeof (ServiceResult_Statistics<>));
			Bind(typeof (IServiceResult_Index<>)).To(typeof (ServiceResult_Index<>));
			Bind(typeof (IServiceResult_Upload<>)).To(typeof (ServiceResult_Upload<>));
			
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