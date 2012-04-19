using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using CHAOS.Events;
using CHAOS.Utilities;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.Managers;
using Object = CHAOS.Portal.Client.Data.MCM.Object;
using CHAOS.Extensions;
using System.Linq;

namespace CHAOS.Portal.Client.Standard.Managers
{
	public class ObjectManager : IObjectManager
	{
		public event EventHandler<DataEventArgs<Exception>> FailedToGetObjectByGUID = delegate { };
		public event EventHandler<DataEventArgs<Exception>> FailedToGetObjects = delegate { };

		private readonly IDictionary<Guid, Object> _Objects;

		private readonly IPortalClient _Client;

		public ObjectManager(IPortalClient client)
		{
			_Client = ArgumentUtilities.ValidateIsNotNull("client", client);
			_Objects = new Dictionary<Guid, Object>();
		}

		#region By GUID

		public Object GetObjectByGUID(Guid guid, bool includeFiles, bool includeMetadata, bool includeObjectRelations)
		{
			if (!_Objects.ContainsKey(guid))
				_Objects[guid] = new Object {GUID = guid};
				
			var result = _Objects[guid];

			var state = _Client.Object.Get(string.Format("GUID:{0}", guid), null, includeMetadata, includeFiles, includeObjectRelations, 0, 1);
			state.Callback = GetObjectByGUIDCompleted;
			state.FeedbackOnDispatcher = true;

			return result;
		}

		private void GetObjectByGUIDCompleted(IServiceResult_MCM<Object> result, Exception error, object token)
		{
			if (error != null)
			{
				FailedToGetObjectByGUID(this, new DataEventArgs<Exception>(error));
				return;
			}

			if(result.MCM.Error != null)
			{
				FailedToGetObjectByGUID(this, new DataEventArgs<Exception>(result.MCM.Error));
				return;
			}

			if(result.MCM.Data.Count != 1)
			{
				FailedToGetObjectByGUID(this, new DataEventArgs<Exception>(new Exception(string.Format("Call to get single object by guid returned {0} objects", result.MCM.Data.Count))));
				return;
			}

			UpdateObject(_Objects[result.MCM.Data[0].GUID], result.MCM.Data[0]);
		}

		#endregion
		#region By File ID

		public Object GetObjectByFileID(int fileID, bool includeFiles, bool includeMetadata, bool includeObjectRelations)
		{
			return _Objects.Values.FirstOrDefault(o => !o.Files.IsNull() && o.Files.Any(f => f.ID == fileID)); //TODO: This is a temporary solution.
		}

		#endregion
		#region By Search

		public IManagerResult<Object> GetObjectBySearch(string query, string sort, bool includeFiles, bool includeMetadata, bool includeObjectRelations, uint pageSize)
		{
			return GetResult(query, sort, includeFiles, includeMetadata, includeObjectRelations, pageSize);
		}

		#endregion
		#region By Folder

		public IManagerResult<Object> GetObjectsByFolder(Folder folder, bool includeFiles, bool includeMetadata, bool includeObjectRelations, uint pageSize)
		{
			return GetObjectsByFolder(folder.ValidateIsNotNull("folder").ID, includeFiles, includeMetadata, includeObjectRelations, pageSize);
		}

		public IManagerResult<Object> GetObjectsByFolder(int folderID, bool includeFiles, bool includeMetadata, bool includeObjectRelations, uint pageSize)
		{
			return GetResult(string.Format("FolderID:{0}", folderID), null, includeFiles, includeMetadata, includeObjectRelations, pageSize);
		}

		#endregion
		#region GetResult

		private IManagerResult<Object> GetResult(string query, string sort, bool includeFiles, bool includeMetadata, bool includeObjectRelations, uint pageSize)
		{
			return new ManagerResult<Object>(pageSize, (i, r) =>
			{
				var state = _Client.Object.Get(query, sort, includeMetadata, includeFiles, includeObjectRelations, (int)i, (int)pageSize);
				state.Token = (Action<IList<Object>, uint>)((os, c) =>
				{
					r.TotalCount = c;
					r.AddResult(i, os);
				});
				state.Callback = GetObjectCompleted;
				state.FeedbackOnDispatcher = true;
			});
		}

		private void GetObjectCompleted(IServiceResult_MCM<Object> result, Exception error, object token)
		{
			if (error != null)
			{
				FailedToGetObjects(this, new DataEventArgs<Exception>(error));
				return;
			}
			if (result.MCM.Error != null)
			{
				FailedToGetObjects(this, new DataEventArgs<Exception>(result.MCM.Error));
				return;
			}

			((Action<IList<Object>, uint>)token)(UpdateObjects(result.MCM.Data), result.MCM.TotalCount);
		}

		#endregion
		#region AddLanguage

		public Metadata AddLanguage(Object @object, MetadataSchema schema, Language language)
		{
			@object.ValidateIsNotNull("@object");
			schema.ValidateIsNotNull("schema");
			language.ValidateIsNotNull("language");

			if(@object.Metadatas.Any(m => m.MetadataSchemaID == schema.ID && m.LanguageCode == language.LanguageCode))
				throw new Exception(string.Format("Object already have metadata with {0} language", language.LanguageCode));

			return AddLanguage(@object, schema.ID, language.LanguageCode);
		}

		public Metadata AddLanguage(Object @object, int schemaID, string languageCode)
		{
			var metadata = new Metadata
			{
				MetadataSchemaID = schemaID,
				LanguageCode = languageCode
			};

			@object.Metadatas.Add(metadata);

			return metadata;
		}

		#endregion
		#region SaveMetadata

		public void SaveMetadata(Metadata metadata, XElement newData)
		{
			metadata.ValidateIsNotNull("metadata");
			newData.ValidateIsNotNull("newData");

			var @object = _Objects.Values.FirstOrDefault(o => o.Metadatas.Contains(metadata));

			if(@object == null)
				throw new Exception("Could not find object matching metadata");

			_Client.Metadata.Set(@object.GUID, metadata.MetadataSchemaID, metadata.LanguageCode, newData);
		}

		#endregion

		private IList<Object> UpdateObjects(IList<Object> objects)
		{
			var result = new List<Object>();
			
			foreach (var newObject in objects)
			{
				if (_Objects.ContainsKey(newObject.GUID))
				{
					var existingObject = _Objects[newObject.GUID];

					UpdateObject(existingObject, newObject);

					result.Add(existingObject);
				}
				else
				{
					_Objects.Add(newObject.GUID, newObject);

					result.Add(newObject);
				}
			}

			return result;
		}

		private static void UpdateObject(Object oldObject, Object newObject)
		{
			oldObject.GUID = newObject.GUID;
			oldObject.ObjectTypeID = newObject.ObjectTypeID;
			oldObject.DateCreated = newObject.DateCreated;

			if (oldObject.Metadatas.IsNull())
				oldObject.Metadatas = newObject.Metadatas;
			else
				UpdateCollection(oldObject.Metadatas, newObject.Metadatas, (m1, m2) => m1.MetadataSchemaID == m2.MetadataSchemaID && m1.LanguageCode == m2.LanguageCode, UpdateMetadata);

			if (oldObject.Files.IsNull())
				oldObject.Files = newObject.Files;
			else
				UpdateCollection(oldObject.Files, newObject.Files, (f1, f2) => f1.URL == f2.URL, UpdateFile);

			if (oldObject.ObjectRelations.IsNull())
				oldObject.ObjectRelations = newObject.ObjectRelations;
			else
				UpdateCollection(oldObject.ObjectRelations, newObject.ObjectRelations, (r1, r2) => true, UpdateObjectRelation);
		}

		private static void UpdateCollection<T>(ObservableCollection<T> oldCollection, ObservableCollection<T> newCollection, Func<T, T, bool> comparer, Action<T, T> updater) where T : class
		{
			if(newCollection.IsNull())
				return;
			
			for (var i = 0; i < oldCollection.Count; i++)
			{
				var olditem = oldCollection[i];

				var newItem = newCollection.FirstOrDefault(item => comparer(olditem, item));

				if (newItem == null)
				{
					oldCollection.RemoveAt(i--);
					continue;
				}

				updater(olditem, newItem);
				newCollection.Remove(newItem);
			}

			foreach (var item in newCollection)
				oldCollection.Add(item);
		}

		private static void UpdateMetadata(Metadata oldMetadata, Metadata newMetadata)
		{
			oldMetadata.DateCreated = newMetadata.DateCreated;
			oldMetadata.DateModified = newMetadata.DateModified;
			oldMetadata.MetadataXML = newMetadata.MetadataXML;
		}

		private static void UpdateFile(File oldFile, File newFile)
		{
			oldFile.Filename = newFile.Filename;
			oldFile.OriginalFilename = newFile.OriginalFilename;
			oldFile.Token = newFile.Token;
			oldFile.URL = newFile.URL;
			oldFile.FormatID = newFile.FormatID;
			oldFile.Format = newFile.Format;
			oldFile.FormatCategory = newFile.FormatCategory;
			oldFile.FormatType = newFile.FormatType;
		}

		private static void UpdateObjectRelation(ObjectRelation oldRelation, ObjectRelation newRelation)
		{
		}
	}
}