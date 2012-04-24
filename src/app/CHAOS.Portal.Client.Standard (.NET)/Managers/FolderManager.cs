using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CHAOS.Events;
using CHAOS.Utilities;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.Managers;
using System.Linq;
using CHAOS.Extensions;

namespace CHAOS.Portal.Client.Standard.Managers
{
	public class FolderManager : IFolderManager
	{
		public event EventHandler<DataEventArgs<uint?>> FailedToGetFolders = delegate { };
		
		private readonly IPortalClient _client;
		private readonly IDictionary<uint?, ObservableCollection<Folder>> _foldersByParent;

		public FolderManager(IPortalClient client)
		{
			_client = ArgumentUtilities.ValidateIsNotNull("client", client);
			_foldersByParent = new Dictionary<uint?, ObservableCollection<Folder>>();
		}

		public ObservableCollection<Folder> GetFolders(Folder parentFolder)
		{
			return GetFolders(parentFolder.DoIfIsNotNull(f => f.ID));
		}

		public ObservableCollection<Folder> GetFolders(uint? parentFolderID)
		{
			lock (_foldersByParent)
			{
				if (!_foldersByParent.ContainsKey(parentFolderID))
					_foldersByParent[parentFolderID] = new ObservableCollection<Folder>();
			}

			var childFolders = _foldersByParent[parentFolderID];

			var state = _client.Folder.Get(null, null, parentFolderID);
			state.Token = parentFolderID;
			state.Callback = GetChildFoldersCompleted;
			state.FeedbackOnDispatcher = true;

			return childFolders;
		}

		private void GetChildFoldersCompleted(IServiceResult_MCM<Folder> result, Exception error, object token)
		{
			var parentID = (uint?) token;

			if(error != null)
			{
				FailedToGetFolders(this, new DataEventArgs<uint?>(parentID));
				return;
			}

			var collection = _foldersByParent[parentID];

			foreach (var newFolder in result.MCM.Data)
			{
				var existingFolder = collection.FirstOrDefault(f => f.ID == newFolder.ID);
				
				if(existingFolder != null)
					UpdateFolder(existingFolder, newFolder);
				else
					collection.Add(newFolder);
			}
		}

		private static void UpdateFolder(Folder oldFolder, Folder newFolder)
		{
			//oldFolder.GUID				= newFolder.GUID;
			oldFolder.ParentID				= newFolder.ParentID;
			oldFolder.FolderTypeID			= newFolder.FolderTypeID;
			//oldFolder.SubscriptionGUID	= newFolder.SubscriptionGUID;
			oldFolder.Title					= newFolder.Title;
			oldFolder.NumberOfSubFolders	= newFolder.NumberOfSubFolders;
			oldFolder.NumberOfObjects		= newFolder.NumberOfObjects;
			//oldFolder.DateCreated			= newFolder.DateCreated;
		}
	}
}