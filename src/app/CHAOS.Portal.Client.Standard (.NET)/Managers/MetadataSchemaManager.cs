using System;
using System.Collections.ObjectModel;
using CHAOS.Events;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.Managers;

namespace CHAOS.Portal.Client.Standard.Managers
{
	public class MetadataSchemaManager : AManager, IMetadataSchemaManager
	{
		public event EventHandler<DataEventArgs<Exception>> ServiceFailed = delegate { };
		public event EventHandler Loaded = delegate { };

		private readonly IPortalClient _client;

		private readonly ObservableCollection<MetadataSchema> _metadataSchemas;
		public ObservableCollection<MetadataSchema> MetadataSchemas { get { return _metadataSchemas; } }

		public MetadataSchemaManager(IPortalClient client)
		{
			_client = client;
			_metadataSchemas = new ObservableCollection<MetadataSchema>();

			if (_client.HasSession)
				GetSchemas();
			else
				_client.SessionAcquired += ClientSessionAquired;
		}

		private void ClientSessionAquired(object sender, EventArgs e)
		{
			_client.SessionAcquired -= ClientSessionAquired;
			GetSchemas();
		}

		private void GetSchemas()
		{
			_client.MetadataSchema.Get(null).Callback = ClientMetadataSchemaGetcompleted;
		}

		private void ClientMetadataSchemaGetcompleted(IServiceResult_MCM<MetadataSchema> result, Exception error, object token)
		{
			if(error != null)
			{
				ServiceFailed(this, new DataEventArgs<Exception>(error));
				return;
			}

			UpdatePublicProperty(() =>
				                     {
										 foreach (var schema in result.MCM.Data)
											 _metadataSchemas.Add(schema);

										 Loaded(this, EventArgs.Empty);
				                     });
		}
	}
}