using System;
using System.Collections.ObjectModel;
using CHAOS.Events;
using CHAOS.Portal.Client.MCM.Data;

namespace CHAOS.Portal.Client.Managers
{
	public interface IFolderManager
	{
		/// <summary>
		/// Is raised when a call to get folders failed, the parent folder id is available on the event args.
		/// </summary>
		event EventHandler<DataEventArgs<uint?>> FailedToGetFolders;
		
		/// <summary>
		/// Gets the child folders of the specified folder, set to null to get root folders.
		/// </summary>
		/// <param name="parentFolder">The parent folder.</param>
		/// <returns>A collection with a cached result, it's updated as soon as newer data is available.</returns>
		ObservableCollection<Folder> GetFolders(Folder parentFolder);
		/// <summary>
		/// Gets the child folders of the specified folder.
		/// </summary>
		/// <param name="parentFolderID">The GUID of the parent folder.</param>
		/// <returns>A collection with a cached result, it's updated as soon as newer data is available.</returns>
		ObservableCollection<Folder> GetFolders(uint? parentFolderID);
	}
}