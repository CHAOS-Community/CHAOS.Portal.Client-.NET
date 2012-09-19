using System;
using System.Collections.ObjectModel;
using System.IO;
using CHAOS.Portal.Client.Managers;
using CHAOS.Extensions;

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

		public IFileUploader Upload(Guid objectGUID, uint formatTypeID, string fileName, Stream fileData)
		{
			var uploader = new FileUploader(_client);
			uploader.Initialize(objectGUID, formatTypeID, fileName.ValidateIsNotNull("fileName"), fileData.ValidateIsNotNull("fileData"));

			_innerFileUploaders.Add(uploader);

			return uploader;
		}
	}
}