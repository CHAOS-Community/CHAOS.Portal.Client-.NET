using System.Collections.ObjectModel;
using CHAOS.Portal.Client.Data.MCM;

namespace CHAOS.Portal.Client.Managers
{
	public interface IMetadataSchemaManager : ILoadingManager
	{
		ObservableCollection<MetadataSchema> MetadataSchemas { get; }
	}
}