using System;
using System.ComponentModel;
using System.IO;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.Managers.Data;

namespace CHAOS.Portal.Client.Standard.Managers.Data
{
	public class FileUploader : IFileUploader
	{
		public event EventHandler Completed = delegate { };

		private readonly IPortalClient _client;
		private Guid _objectGUID;
		private uint _formatID;
		private Stream _data;
		private double _progress;
		private TransactionState _state;
		private UploadToken _uploadToken;
		private byte[] _buffer;

		#region Properties

		public double Progress
		{
			get { return _progress; }
			private set
			{
				_progress = value;
				RaisePropertyChanged("Progress");
			}
		}

		public TransactionState State
		{
			get { return _state; }
			private set
			{
				_state = value;

				if(_state == TransactionState.Completed || _state == TransactionState.Failed)
				{
					Completed(this, EventArgs.Empty);

					if(_data != null)
					{
						_data.Close();
						_data = null;
					}
				}

				RaisePropertyChanged("State");
			}
		}

		private uint ChunkIndex
		{
			get
			{
				if (_data == null || _uploadToken == null) return 0;
				return (uint)(_data.Position / _uploadToken.ChunkSize);
			}
		}
		
		#endregion

		public FileUploader(IPortalClient client)
		{
			_client = client;
		}

		public void Initialize(Guid objectGUID, uint formatID, Stream data)
		{
			_objectGUID = objectGUID;
			_formatID = formatID;
			_data = data;
		}

		#region Upload Start

		public void Start()
		{
			State = TransactionState.Started;
			_client.Upload.Initiate(_objectGUID, _formatID, (ulong)_data.Length, true).Callback = InitiateCompleted;
		}

		private void InitiateCompleted(IServiceResult_MCM<UploadToken> result, Exception error, object token)
		{
			if (error != null)
			{
				State = TransactionState.Failed;
				return;
			}

			_uploadToken = result.MCM.Data[0];

			_buffer = new byte[_uploadToken.ChunkSize];

			UploadNextChunk();
		}
		
		#endregion
		#region Chunk Upload

		private void UploadNextChunk()
		{
			var chunkIndex = ChunkIndex;

			_data.Read(_buffer, 0, _buffer.Length);

			_client.Upload.Transfer(_uploadToken.UploadID, chunkIndex, _buffer).WithCallback(TransferCompleted).UploadProgressChanged += (sender, args) => Progress = (ChunkIndex - 1 + args.NewValue) / _uploadToken.NoOfChunks;
		}

		private void TransferCompleted(IServiceResult_MCM<ScalarResult> result, Exception error, object token)
		{
			if (error != null)
			{
				State = TransactionState.Failed;
				return;
			}

			Progress = (double)ChunkIndex /_uploadToken.NoOfChunks;

			UploadNextChunk();
		}

		#endregion
		#region PropertyChanged

		public event PropertyChangedEventHandler PropertyChanged = delegate { };
		private void RaisePropertyChanged(string propertyName)
		{
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
		
		#endregion
	}
}