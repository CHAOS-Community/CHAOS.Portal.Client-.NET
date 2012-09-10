using System;
using System.Collections.ObjectModel;
using System.IO;
using CHAOS.Portal.Client.Managers.Data;

namespace CHAOS.Portal.Client.Managers
{
	public interface IFileUploadManager
	{
		ReadOnlyObservableCollection<IFileUploader> FileUploaders { get; }
		IFileUploader Upload(Guid objectGUID, uint formatID, Stream data);
	}
}