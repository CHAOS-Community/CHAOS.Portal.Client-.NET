using System;
using System.Collections.ObjectModel;
using CHAOS.Events;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.Managers;

namespace CHAOS.Portal.Client.Standard.Managers
{
	public class MCMTypesManager : IMCMTypesManager
	{
		public event EventHandler<DataEventArgs<Exception>> ServiceFailed = delegate { };
		
		private readonly IPortalClient _client;
		private readonly ObservableCollection<FolderType> _folderTypes;
		private readonly ObservableCollection<FormatType> _formatTypes;
		private readonly ObservableCollection<ObjectRelationType> _objectRelationTypes;
		private readonly ObservableCollection<ObjectType> _objectTypes;

		public ObservableCollection<FolderType> FolderTypes						{ get { return _folderTypes; } }
		public ObservableCollection<FormatType> FormatTypes						{ get { return _formatTypes; } }
		public ObservableCollection<ObjectRelationType> ObjectRelationTypes		{ get { return _objectRelationTypes; } }
		public ObservableCollection<ObjectType> ObjectTypes						{ get { return _objectTypes; } }

		public MCMTypesManager(IPortalClient client)
		{
			_client = client;
			_folderTypes = new ObservableCollection<FolderType>();
			_formatTypes = new ObservableCollection<FormatType>();
			_objectRelationTypes = new ObservableCollection<ObjectRelationType>();
			_objectTypes = new ObservableCollection<ObjectType>();

			if (_client.HasSession)
				GetTypes();
			else
				_client.SessionAcquired += ClientSessionAquired;
		}

		private void ClientSessionAquired(object sender, EventArgs e)
		{
			_client.SessionAcquired -= ClientSessionAquired;
			GetTypes();
		}

		private void GetTypes()
		{
			_client.FolderType.Get(null, null).Callback = ClientFolderTypeGet;
			_client.FormatType.Get(null, null).Callback = ClientFormatTypeGet;
			_client.ObjectRelationType.Get(null, null).Callback = ClientObjectRelationTypeGet;
			_client.ObjectType.Get(null, null).Callback = ClientObjectTypeGet;
		}

		private void ClientFolderTypeGet(IServiceResult_MCM<FolderType> result, Exception error, object token)
		{
			if(error != null)
			{
				ServiceFailed(this, new DataEventArgs<Exception>(error));
				return;
			}

			foreach (var folderType in result.MCM.Data)
				_folderTypes.Add(folderType);
		}

		private void ClientFormatTypeGet(IServiceResult_MCM<FormatType> result, Exception error, object token)
		{
			if (error != null)
			{
				ServiceFailed(this, new DataEventArgs<Exception>(error));
				return;
			}

			foreach (var formatType in result.MCM.Data)
				_formatTypes.Add(formatType);
		}

		private void ClientObjectRelationTypeGet(IServiceResult_MCM<ObjectRelationType> result, Exception error, object token)
		{
			if (error != null)
			{
				ServiceFailed(this, new DataEventArgs<Exception>(error));
				return;
			}

			foreach (var objectRelationType in result.MCM.Data)
				_objectRelationTypes.Add(objectRelationType);
		}

		private void ClientObjectTypeGet(IServiceResult_MCM<ObjectType> result, Exception error, object token)
		{
			if (error != null)
			{
				ServiceFailed(this, new DataEventArgs<Exception>(error));
				return;
			}

			foreach (var objectType in result.MCM.Data)
				_objectTypes.Add(objectType);
		}
	}
}