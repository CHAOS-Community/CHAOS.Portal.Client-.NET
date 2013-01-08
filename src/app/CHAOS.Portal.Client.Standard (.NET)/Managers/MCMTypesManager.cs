using System;
using System.Collections.ObjectModel;
using CHAOS.Events;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.MCM.Extensions;
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
			_client.FolderType().Get().Callback = ClientFolderTypeGet;
			_client.FormatType().Get().Callback = ClientFormatTypeGet;
			_client.ObjectRelationType().Get().Callback = ClientObjectRelationTypeGet;
			_client.ObjectType().Get().Callback = ClientObjectTypeGet;
		}

		private void ClientFolderTypeGet(ServiceResponse<FolderType> response, object token)
		{
			if (response.Error != null)
			{
				ServiceFailed(this, new DataEventArgs<Exception>(response.Error));
				return;
			}

			foreach (var folderType in response.Result.Results)
				_folderTypes.Add(folderType);
		}

		private void ClientFormatTypeGet(ServiceResponse<FormatType> response, object token)
		{
			if (response.Error != null)
			{
				ServiceFailed(this, new DataEventArgs<Exception>(response.Error));
				return;
			}

			foreach (var formatType in response.Result.Results)
				_formatTypes.Add(formatType);
		}

		private void ClientObjectRelationTypeGet(ServiceResponse<ObjectRelationType> response, object token)
		{
			if (response.Error != null)
			{
				ServiceFailed(this, new DataEventArgs<Exception>(response.Error));
				return;
			}

			foreach (var objectRelationType in response.Result.Results)
				_objectRelationTypes.Add(objectRelationType);
		}

		private void ClientObjectTypeGet(ServiceResponse<ObjectType> response, object token)
		{
			if (response.Error != null)
			{
				ServiceFailed(this, new DataEventArgs<Exception>(response.Error));
				return;
			}

			foreach (var objectType in response.Result.Results)
				_objectTypes.Add(objectType);
		}
	}
}