using System.Collections.ObjectModel;
using CHAOS.Portal.Client.MCM.Data;

namespace CHAOS.Portal.Client.Managers
{
	public interface IMetadataSchemaManager : ILoadingManager
	{
		ObservableCollection<MetadataSchema> MetadataSchemas { get; }
	}
}