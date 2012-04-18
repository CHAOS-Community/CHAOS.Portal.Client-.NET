using System;
using System.Collections.ObjectModel;
using CHAOS.Common.Events;
using CHAOS.Portal.Client.Data.MCM;

namespace CHAOS.Portal.Client.Managers
{
	public interface IMetadataSchemaManager
	{
		event EventHandler<DataEventArgs<Exception>> ServiceFailed;

		ObservableCollection<MetadataSchema> MetadataSchemas { get; }
	}
}