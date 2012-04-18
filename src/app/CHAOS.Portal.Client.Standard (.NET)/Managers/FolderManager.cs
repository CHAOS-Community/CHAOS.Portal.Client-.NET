using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CHAOS.Common.Events;
using CHAOS.Common.Utilities;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.Managers;
using System.Linq;

namespace CHAOS.Portal.Client.Standard.Managers
{
	public class FolderManager : IFolderManager
	{
		private const int ROOT_FOLDER_ID = -1;

		public event EventHandler<DataEventArgs<int>> FailedToGetFolders = delegate { };
		
		private readonly IPortalClient _Client;
		private readonly IDictionary<int, ObservableCollection<Folder>> _FoldersByParent;

		public FolderManager(IPortalClient client)
		{
			_Client = ArgumentUtilities.ValidateIsNotNull("client", client);
			_FoldersByParent = new Dictionary<int, ObservableCollection<Folder>>();
		}

		public ObservableCollection<Folder> GetFolders(Folder parentFolder)
		{
			return GetFolders(parentFolder == null ? ROOT_FOLDER_ID : parentFolder.ID);
		}

		public ObservableCollection<Folder> GetFolders(int parentFolderID)
		{
			lock (_FoldersByParent)
			{
				if (!_FoldersByParent.ContainsKey(parentFolderID))
					_FoldersByParent[parentFolderID] = new ObservableCollection<Folder>();
			}

			var childFolders = _FoldersByParent[parentFolderID];

			var state = _Client.Folder.Get(null, null, parentFolderID == ROOT_FOLDER_ID ? null : (int?)parentFolderID);
			state.Token = parentFolderID;
			state.Callback = GetChildFoldersCompleted;
			state.FeedbackOnDispatcher = true;

			return childFolders;
		}

		private void GetChildFoldersCompleted(IServiceResult_MCM<Folder> result, Exception error, object token)
		{
			var parentID = (int) token;

			if(error != null)
			{
				FailedToGetFolders(this, new DataEventArgs<int>(parentID));
				return;
			}

			var collection = _FoldersByParent[parentID];

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
			//oldFolder.ID					= newFolder.ID;
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