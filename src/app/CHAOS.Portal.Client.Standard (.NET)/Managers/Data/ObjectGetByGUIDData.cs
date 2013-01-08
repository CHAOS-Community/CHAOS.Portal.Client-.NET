using System;
using System.Collections.Generic;
using Object = CHAOS.Portal.Client.MCM.Data.Object;

namespace CHAOS.Portal.Client.Standard.Managers.Data
{
	public class ObjectGetByGUIDData
	{
		private readonly IList<Action<Object, Exception>> _callbacks;
		private Object _object;
		private Exception _error;

		public Guid GUID { get; private set; }
		public bool IncludeFiles { get; private set; }
		public bool IncludeMetadata { get; private set; }
		public bool IncludeObjectRelations { get; private set; }
		public bool IncludeAccessPoints { get; private set; }

		public DateTime Created { get; private set; }

		public ObjectGetByGUIDData(Guid guid, bool includeFiles, bool includeMetadata, bool includeObjectRelations, bool includeAccessPoints)
		{
			GUID = guid;
			IncludeFiles = includeFiles;
			IncludeMetadata = includeMetadata;
			IncludeObjectRelations = includeObjectRelations;
			IncludeAccessPoints = includeAccessPoints;
			_callbacks = new List<Action<Object, Exception>>();
			Created = DateTime.Now;
		}

		public void AddCallback(Action<Object, Exception> callback)
		{
			lock (_callbacks)
			{
				if (_object == null && _error == null)
				{
					_callbacks.Add(callback);
					return;
				}
			}
			callback(_object, _error);
		}

		public void Call(Object @object, Exception error)
		{
			lock (_callbacks)
			{
				_object = @object;
				_error = error;
			}

			foreach (var callback in _callbacks)
				callback(_object, _error);

			_callbacks.Clear();
		}
	}
}