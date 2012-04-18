using System;
using System.Collections.ObjectModel;
using CHAOS.Common.Events;
using CHAOS.Portal.Client.Data.MCM;

namespace CHAOS.Portal.Client.Managers
{
	public interface IMCMTypesManager
	{
		event EventHandler<DataEventArgs<Exception>> ServiceFailed;
		
		ObservableCollection<FolderType> FolderTypes { get; }
		ObservableCollection<FormatType> FormatTypes { get; }
		ObservableCollection<ObjectRelationType> ObjectRelationTypes { get; }
		ObservableCollection<ObjectType> ObjectTypes { get; }
	}
}