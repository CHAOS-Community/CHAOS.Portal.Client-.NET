using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace CHAOS.Portal.Client.Managers
{
	public interface IManagerResult<T> : IList<T>, INotifyCollectionChanged, INotifyPropertyChanged
	{
		/// <summary>
		/// The total amount of items available from the service.
		/// </summary>
		uint TotalCount { get; }
		/// <summary>
		/// Gets the next page of items from the service and adds them to the collection.
		/// </summary>
		/// <returns>Returns <code>True</code> if there is another page to get.</returns>
		bool GetNextPage();
	}
}