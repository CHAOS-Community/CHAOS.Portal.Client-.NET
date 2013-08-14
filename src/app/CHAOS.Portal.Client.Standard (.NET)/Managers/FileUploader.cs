using System;
using System.IO;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Managers;
using CHAOS.Extensions;
using CHAOS.Portal.Client.Upload.Data;
using CHAOS.Portal.Client.Upload.Extensions;

namespace CHAOS.Portal.Client.Standard.Managers
{
	public class FileUploader : AViewModel, IFileUploader
	{
		public event EventHandler Completed = delegate { };

		private readonly IPortalClient _client;
		private Stream _fileData;
		private double _progress;
		private TransactionState _state;
		private UploadToken _uploadToken;

		#region Properties

		public TransactionState State
		{
			get { return _state; }
			private set
			{
				_state = value;

				if (_state == TransactionState.Completed || _state == TransactionState.Failed)
				{
					Completed(this, EventArgs.Empty);

					if (_fileData != null)
					{
						_fileData.Close();
						_fileData = null;
					}
				}

				RaisePropertyChanged("State");
			}
		}

		public double Progress
		{
			get { return _progress; }
			private set
			{
				_progress = value;
				RaisePropertyChanged("Progress");

				if(Progress == 1d)
					State = TransactionState.Completed;
			}
		}

		public Guid ObjectGUID { get; private set; }
		public uint FormatTypeID { get; private set; }
		public string FileName { get; private set; }
		public ulong FileSize  { get { return (ulong) _fileData.DoIfIsNotNull(d => d.Length); } }

		private uint ChunkIndex
		{
			get
			{
				if (_fileData == null || _uploadToken == null) return 0;
				return (uint) Math.Ceiling((double)_fileData.Position / _uploadToken.ChunkSize);
			}
		}
		
		#endregion

		public FileUploader(IPortalClient client)
		{
			_client = client;
		}

		public void Initialize(Guid objectGUID, uint formatTypeID, string fileName, Stream fileData)
		{
			ObjectGUID = objectGUID;
			FormatTypeID = formatTypeID;
			FileName = fileName;
			_fileData = fileData;
		}

		#region Upload Start

		public void Start()
		{
			State = TransactionState.Started;
			_client.Upload().Initiate(ObjectGUID, FormatTypeID, (ulong)_fileData.Length, true).Callback = InitiateCompleted;
		}

		private void InitiateCompleted(ServiceResponse<PagedResult<UploadToken>> response, object token)
		{
			if (response.Error != null || response.Result.Results.Count == 0)
			{
				State = TransactionState.Failed;
				return;
			}

			_uploadToken = response.Result.Results[0];

			UploadNextChunk();
		}
		
		#endregion
		#region Chunk Upload

		private void UploadNextChunk()
		{
			var chunkIndex = ChunkIndex;

			if(State != TransactionState.Started)
				return;

			_client.Upload().Transfer(_uploadToken.UploadID, chunkIndex, new FileData(FileName, _fileData, _fileData.Position, _uploadToken.ChunkSize)).WithCallback(TransferCompleted).UploadProgressChanged 
				+= (sender, args) => Progress = (ChunkIndex - 1 + Math.Min(0.99, args.NewValue)) / _uploadToken.NoOfChunks; //Math.Min() is used to prevent progress from reaching completed
		}

		private void TransferCompleted(ServiceResponse<PagedResult<ScalarResult>> response, object token)
		{
			if (response.Error != null || response.Result.Results[0].Value != 1)
			{
				State = TransactionState.Failed;
				return;
			}

			Progress = (double)ChunkIndex /_uploadToken.NoOfChunks;

			UploadNextChunk();
		}

		#endregion
	}
}