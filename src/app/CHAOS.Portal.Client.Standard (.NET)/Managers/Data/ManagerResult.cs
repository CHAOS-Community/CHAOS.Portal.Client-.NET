using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CHAOS.Portal.Client.Managers.Data;

namespace CHAOS.Portal.Client.Standard.Managers.Data
{
	public class ManagerResult<T> : ObservableCollection<T>, IManagerResult<T>
	{
		private readonly Action<uint, ManagerResult<T>> _resultsGetter;

		private readonly object _syncObject;

		private uint _totalCount;
		public uint TotalCount
		{
			get { return _totalCount; }
			set
			{
				_totalCount = value;
				RaisePropertyChanged("TotalCount");
			}
		}
		
		private readonly uint _PageSize;
		private uint _NextPageIndex;

		public ManagerResult(uint pageSize, Action<uint, ManagerResult<T>> resultsGetter)
		{
			_PageSize = pageSize;
			_resultsGetter = resultsGetter;
			_syncObject = new object();
		}

		public bool GetNextPage()
		{
			lock (_syncObject)
			{
				if (TotalCount != 0 && _NextPageIndex * _PageSize >= TotalCount)
					return false;

				_resultsGetter(_NextPageIndex++, this);

				return true;
			}
		}

		public void AddResult(uint pageIndex, IList<T> result)
		{
			if (result == null)
				return;

			lock (_syncObject)
			{
				var startIndex = Math.Min(pageIndex * _PageSize, Count);

				foreach (var item in result)
					Insert((int) startIndex++, item);
			}
		}

		#region INotifyPropertyChanged

		private void RaisePropertyChanged(string propertyName)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		#endregion	
	}
}