using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using CHAOS.Portal.Client.Managers;

namespace CHAOS.Portal.Client.Standard.Managers
{
	public class ManagerResult<T> : ObservableCollection<T>, IManagerResult<T>
	{
		private readonly Action<uint, ManagerResult<T>> _ResultsGetter;

		private readonly object _SyncObject;

		private uint _TotalCount;
		public uint TotalCount
		{
			get { return _TotalCount; }
			set
			{
				_TotalCount = value;
				RaisePropertyChanged("TotalCount");
			}
		}
		
		private readonly uint _PageSize;
		private uint _NextPageIndex;

		public ManagerResult(uint pageSize, Action<uint, ManagerResult<T>> resultsGetter)
		{
			_PageSize = pageSize;
			_ResultsGetter = resultsGetter;
			_SyncObject = new object();
		}

		public bool GetNextPage()
		{
			lock (_SyncObject)
			{
				if (TotalCount != 0 && _NextPageIndex * _PageSize >= TotalCount)
					return false;

				_ResultsGetter(_NextPageIndex++, this);

				return true;
			}
		}

		public void AddResult(uint pageIndex, IList<T> result)
		{
			lock (_SyncObject)
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