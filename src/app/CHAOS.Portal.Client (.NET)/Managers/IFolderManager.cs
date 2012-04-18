using System;
using System.Collections.ObjectModel;
using CHAOS.Common.Events;
using CHAOS.Portal.Client.Data.MCM;

namespace CHAOS.Portal.Client.Managers
{
	public interface IFolderManager
	{
		/// <summary>
		/// Is raised when a call to get folders failed, the parent folder id is available on the event args.
		/// </summary>
		event EventHandler<DataEventArgs<int>> FailedToGetFolders;
		
		/// <summary>
		/// Gets the child folders of the specified folder, set to null to get root folders.
		/// </summary>
		/// <param name="parentFolder">The parent folder.</param>
		/// <returns>A collection with a cached result, it's updated as soon as newer data is available.</returns>
		ObservableCollection<Folder> GetFolders(Folder parentFolder);
		/// <summary>
		/// Gets the child folders of the specified folder.
		/// </summary>
		/// <param name="parentFolderID">The ID of the parent folder.</param>
		/// <returns>A collection with a cached result, it's updated as soon as newer data is available.</returns>
		ObservableCollection<Folder> GetFolders(int parentFolderID);
	}
}