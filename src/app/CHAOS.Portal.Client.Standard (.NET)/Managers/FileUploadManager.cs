using System;
using System.Collections.ObjectModel;
using System.IO;
using CHAOS.Portal.Client.Managers;

namespace CHAOS.Portal.Client.Standard.Managers
{
	public class FileUploadManager : IFileUploadManager
	{
		private readonly IPortalClient _client;

		private readonly ObservableCollection<IFileUploader> _innerFileUploaders;
		private readonly ReadOnlyObservableCollection<IFileUploader> _fileUploaders;
		public ReadOnlyObservableCollection<IFileUploader> FileUploaders { get { return _fileUploaders; } }

		public FileUploadManager(IPortalClient client)
		{
			_client = client;
			_innerFileUploaders = new ObservableCollection<IFileUploader>();
			_fileUploaders = new ReadOnlyObservableCollection<IFileUploader>(_innerFileUploaders); 
		}

		public IFileUploader Upload(Guid objectGUID, uint formatID, Stream data)
		{
			var uploader = new FileUploader(_client);
			uploader.Initialize(objectGUID, formatID, data);

			_innerFileUploaders.Add(uploader);

			return uploader;
		}
	}
}