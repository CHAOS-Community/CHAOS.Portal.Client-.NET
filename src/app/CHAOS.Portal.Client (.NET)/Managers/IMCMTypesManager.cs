using System;
using System.Collections.ObjectModel;
using CHAOS.Events;
using CHAOS.Portal.Client.MCM.Data;

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