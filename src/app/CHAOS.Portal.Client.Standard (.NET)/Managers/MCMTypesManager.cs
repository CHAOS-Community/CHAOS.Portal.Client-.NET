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
		
		private readonly IPortalClient _Client;
		private readonly ObservableCollection<FolderType> _FolderTypes;
		private readonly ObservableCollection<FormatType> _FormatTypes;
		private readonly ObservableCollection<ObjectRelationType> _ObjectRelationTypes;
		private readonly ObservableCollection<ObjectType> _ObjectTypes;

		public ObservableCollection<FolderType> FolderTypes						{ get { return _FolderTypes; } }
		public ObservableCollection<FormatType> FormatTypes						{ get { return _FormatTypes; } }
		public ObservableCollection<ObjectRelationType> ObjectRelationTypes		{ get { return _ObjectRelationTypes; } }
		public ObservableCollection<ObjectType> ObjectTypes						{ get { return _ObjectTypes; } }

		public MCMTypesManager(IPortalClient client)
		{
			_Client = client;
			_FolderTypes = new ObservableCollection<FolderType>();
			_FormatTypes = new ObservableCollection<FormatType>();
			_ObjectRelationTypes = new ObservableCollection<ObjectRelationType>();
			_ObjectTypes = new ObservableCollection<ObjectType>();

			if (_Client.HasSession)
				GetTypes();
			else
				_Client.SessionAcquired += ClientSessionAquired;
		}

		private void ClientSessionAquired(object sender, EventArgs e)
		{
			_Client.SessionAcquired -= ClientSessionAquired;
			GetTypes();
		}

		private void GetTypes()
		{
			_Client.FolderType.Get(null, null).Callback = ClientFolderTypeGet;
			_Client.FormatType.Get(null, null).Callback = ClientFormatTypeGet;
			_Client.ObjectRelationType.Get(null, null).Callback = ClientObjectRelationTypeGet;
			_Client.ObjectType.Get(null, null).Callback = ClientObjectTypeGet;
		}

		private void ClientFolderTypeGet(IServiceResult_MCM<FolderType> result, Exception error, object token)
		{
			if(error != null)
			{
				ServiceFailed(this, new DataEventArgs<Exception>(error));
				return;
			}

			foreach (var folderType in result.MCM.Data)
				_FolderTypes.Add(folderType);
		}

		private void ClientFormatTypeGet(IServiceResult_MCM<FormatType> result, Exception error, object token)
		{
			if (error != null)
			{
				ServiceFailed(this, new DataEventArgs<Exception>(error));
				return;
			}

			foreach (var formatType in result.MCM.Data)
				_FormatTypes.Add(formatType);
		}

		private void ClientObjectRelationTypeGet(IServiceResult_MCM<ObjectRelationType> result, Exception error, object token)
		{
			if (error != null)
			{
				ServiceFailed(this, new DataEventArgs<Exception>(error));
				return;
			}

			foreach (var objectRelationType in result.MCM.Data)
				_ObjectRelationTypes.Add(objectRelationType);
		}

		private void ClientObjectTypeGet(IServiceResult_MCM<ObjectType> result, Exception error, object token)
		{
			if (error != null)
			{
				ServiceFailed(this, new DataEventArgs<Exception>(error));
				return;
			}

			foreach (var objectType in result.MCM.Data)
				_ObjectTypes.Add(objectType);
		}
	}
}