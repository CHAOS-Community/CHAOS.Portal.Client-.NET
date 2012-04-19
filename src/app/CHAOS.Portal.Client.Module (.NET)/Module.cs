using System.Collections.Generic;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Managers;
using CHAOS.Portal.Client.Standard;
using CHAOS.Portal.Client.Standard.Data;
using CHAOS.Portal.Client.Standard.Managers;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Serialization.String;
using CHAOS.Serialization.XML;
using CHAOS.Serialization.Standard.String;
using CHAOS.Serialization.Standard.XML;
using Ninject;
using Ninject.Modules;

namespace CHAOS.Portal.Client.Module
{
	public class Module : NinjectModule
	{
		public override void Load()
		{
			Bind(typeof (IModuleResult<>)).To(typeof (ModuleResult<>));
			Bind(typeof (IServiceResult)).To(typeof (ServiceResult));
			Bind(typeof (IServiceResult_Portal<>)).To(typeof (ServiceResult_Portal<>));
			Bind(typeof (IServiceResult_MCM<>)).To(typeof (ServiceResult_MCM<>));
			
			Bind(typeof (IServiceCall<>)).To(typeof(ServiceCall<>));
			Bind<IServiceCallFactory>().To<ServiceCallFactory>().InSingletonScope();
			Bind<IPortalClient>().To<PortalClient>().InSingletonScope();
			
			Bind<IFolderManager>().To<FolderManager>().InSingletonScope();
			Bind<IObjectManager>().To<ObjectManager>().InSingletonScope();
			Bind<IFileManager>().To<FileManager>().InSingletonScope();
			Bind<IMetadataSchemaManager>().To<MetadataSchemaManager>().InSingletonScope();
			Bind<IMCMTypesManager>().To<MCMTypesManager>().InSingletonScope();
			Bind<ILanguageManager>().To<LanguageManager>().InSingletonScope();
			Bind<IUserManager>().To<UserManager>().InSingletonScope();

			/************** Serializer ************/
			Bind<IStringSerializer>().To<StringSerializer>().InSingletonScope();
			Bind<IXMLSerializer>().To<XMLSerializer>().InSingletonScope();

			Kernel.Get<IXMLSerializer>().Map(typeof(IList<>), typeof(List<>));
		}
	}
}