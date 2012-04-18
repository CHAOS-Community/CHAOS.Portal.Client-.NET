using System;
using System.Collections.ObjectModel;
using CHAOS.Common.Events;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.Managers;

namespace CHAOS.Portal.Client.Standard.Managers
{
	public class MetadataSchemaManager : IMetadataSchemaManager
	{
		public event EventHandler<DataEventArgs<Exception>> ServiceFailed = delegate { };

		private readonly IPortalClient _Client;

		private readonly ObservableCollection<MetadataSchema> _MetadataSchemas;
		public ObservableCollection<MetadataSchema> MetadataSchemas { get { return _MetadataSchemas; } }

		public MetadataSchemaManager(IPortalClient client)
		{
			_Client = client;
			_MetadataSchemas = new ObservableCollection<MetadataSchema>();

			if (_Client.HasSession)
				GetSchemas();
			else
				_Client.SessionAcquired += ClientSessionAquired;
		}

		private void ClientSessionAquired(object sender, EventArgs e)
		{
			_Client.SessionAcquired -= ClientSessionAquired;
			GetSchemas();
		}

		private void GetSchemas()
		{
			_Client.MetadataSchema.Get(null).Callback = ClientMetadataSchemaGetcompleted;
		}

		private void ClientMetadataSchemaGetcompleted(IServiceResult_MCM<MetadataSchema> result, Exception error, object token)
		{
			if(error != null)
			{
				ServiceFailed(this, new DataEventArgs<Exception>(error));
				return;
			}

			foreach (var schema in result.MCM.Data)
				_MetadataSchemas.Add(schema);
		}
	}
}