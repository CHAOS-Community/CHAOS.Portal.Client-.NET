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

		private readonly IDictionary<Guid, Object> _objects;

		private readonly IPortalClient _client;

		public ObjectManager(IPortalClient client)
		{
			_client = ArgumentUtilities.ValidateIsNotNull("client", client);
			_objects = new Dictionary<Guid, Object>();
		}
		#region Create

		public Object Create<T>(uint objectTypeID, uint folderID, Guid? guid, Action<bool, T> callback = null, T token = default(T))
		{
			var @object = new Object();

			if (guid.HasValue)
			{
				@object.GUID = guid.Value;
				_objects[guid.Value] = @object;
			}

			var state = _client.Object.Create(guid, objectTypeID, folderID);
			state.Callback = CreateCompleted;
			state.Token = new CallbackToken<Object, T>(@object, token, callback);

			return @object;
		}

		private void CreateCompleted(IServiceResult_MCM<Object> result, Exception error, object token)
		{
			var callbackToken = (ICallbackToken<Object>) token;

			if(error == null && result.MCM.Data.Count == 1)
			{
				UpdateObject(callbackToken.InternalToken, result.MCM.Data[0]);

				if (!_objects.ContainsKey(callbackToken.InternalToken.GUID))
					_objects[callbackToken.InternalToken.GUID] = callbackToken.InternalToken;

				callbackToken.CallCallback(true);
			}
			else
			{
				if (!_objects.ContainsKey(callbackToken.InternalToken.GUID))
					_objects.Remove(callbackToken.InternalToken.GUID);

				callbackToken.CallCallback(false);
			}	
		}

		#endregion
		#region Delete

		public void Delete(Object @object, Action<bool> callback = null)
		{
			Delete(@object.ValidateIsNotNull("@object").GUID, callback);
		}

		public void Delete(Guid objectGuid, Action<bool> callback = null)
		{
			Delete(objectGuid, callback == null ? null : (Action<bool, object>)((s, t) => callback(s)), null);
		}

		public void Delete<T>(Object @object, Action<bool, T> callback, T token)
		{
			Delete(@object.ValidateIsNotNull("@object").GUID, callback, token);
		}

		public void Delete<T>(Guid objectGuid, Action<bool, T> callback, T token)
		{
			_client.Object.Delete(objectGuid).WithCallback((result, error, o) =>
			                                               	{
			                                               		if(error == null)
			                                               		{
			                                               			_objects.Remove(objectGuid);
			                                               			callback(true, (T) o);
			                                               		}
																	callback(false, (T)o);
																
			                                               	}, token);
		}

		#endregion
		#region ObjectRelation

		public void CreateRelation<T>(Object object1, Object object2, ObjectRelationType relationType, int? sequence, Action<bool, T> callback = null, T token = default(T))
		{
			CreateRelation(object1.ValidateIsNotNull("object1").GUID, object2.ValidateIsNotNull("object2").GUID, relationType.ValidateIsNotNull("relationType").ID, sequence, callback, token);
		}

		public void CreateRelation<T>(Guid object1, Guid object2, uint relationType, int? sequence, Action<bool, T> callback = null, T token = default(T))
		{
			var state = _client.ObjectRelation.Create(object1, object2, relationType, sequence);

			state.Callback += CreateRelationCompleted;
			state.Token = new CallbackToken<IList<Guid>, T>(new List<Guid> {object1, object2}, token, callback);
		}

		private void CreateRelationCompleted(IServiceResult_MCM<ScalarResult> result, Exception error, object token)
		{
			var callbackToken = (ICallbackToken<IList<Guid>>)token;

			if (error == null && result.MCM.Data.Count == 1)
			{
				GetObjectByGUID(callbackToken.InternalToken[0], false, false, true, false); //TODO: Make the new relation manually
				GetObjectByGUID(callbackToken.InternalToken[1], false, false, true, false); //TODO: Make the new relation manually

				callbackToken.CallCallback(true);
			}
			else
				callbackToken.CallCallback(false);
		}

		#endregion
		#region By GUID

		public Object GetObjectByGUID(Guid guid, bool includeFiles, bool includeMetadata, bool includeObjectRelations, bool includeAccessPoints)
		{
			if (!_objects.ContainsKey(guid))
				_objects[guid] = new Object {GUID = guid};
				
			var result = _objects[guid];

			var state = _client.Object.Get(string.Format("GUID:{0}", guid), null, 0, 1, includeMetadata, includeFiles, includeObjectRelations, includeAccessPoints);
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

			UpdateObject(_objects[result.MCM.Data[0].GUID], result.MCM.Data[0]);
		}

		#endregion
		#region By File GUID

		public Object GetObjectByFileID(int fileID, bool includeFiles, bool includeMetadata, bool includeObjectRelations, bool includeAccessPoints)
		{
			return _objects.Values.FirstOrDefault(o => o.Files != null && o.Files.Any(f => f.ID == fileID)); //TODO: This is a temporary solution.
		}

		#endregion
		#region By Metadata

		public Object GetObjectByMetadata(Metadata metadata)
		{
			var @object = _objects.Values.FirstOrDefault(o => o.Metadatas.Contains(metadata));

			if(@object == null)
				throw new Exception("Object not found");

			return @object;
		}

		#endregion
		#region By Search

		public IManagerResult<Object> GetObjectBySearch(string query, string sort, uint pageSize, bool includeFiles, bool includeMetadata, bool includeObjectRelations, bool includeAccessPoints)
		{
			return GetResult(query, sort, pageSize, includeFiles, includeMetadata, includeObjectRelations, includeAccessPoints);
		}

		#endregion
		#region By Folder

		public IManagerResult<Object> GetObjectsByFolder(Folder folder, uint pageSize, bool includeFiles, bool includeMetadata, bool includeObjectRelations, bool includeAccessPoints)
		{
			return GetObjectsByFolder(folder.ValidateIsNotNull("folder").ID, pageSize, includeFiles, includeMetadata, includeObjectRelations, includeAccessPoints);
		}

		public IManagerResult<Object> GetObjectsByFolder(uint folderID, uint pageSize, bool includeFiles, bool includeMetadata, bool includeObjectRelations, bool includeAccessPoints)
		{
			return GetResult(string.Format("FolderID:{0}", folderID), null, pageSize, includeFiles, includeMetadata, includeObjectRelations, includeAccessPoints);
		}

		#endregion
		#region Link

		public void MoveLinkToFolder<T>(Object @object, Folder fromFolder, Folder toFolder, Action<bool, T> callback = null, T token = default(T))
		{
			MoveLinkToFolder(@object.GUID, fromFolder.ID, toFolder.ID, callback, token);
		}

		public void MoveLinkToFolder<T>(Guid objectGUID, uint fromFolderID, uint toFolderID, Action<bool, T> callback = null, T token = default(T))
		{
			_client.Link.Update(objectGUID, fromFolderID, toFolderID).Callback = (result, error, t) =>
			                                                                    {
			                                                                        if (callback != null)
			                                                                            callback(error == null, token);
			                                                                    };
		}

		public void CreateLinkInFolder<T>(Object @object, Folder folder, Action<bool, T> callback = null, T token = default(T))
		{
			CreateLinkInFolder(@object.GUID, folder.ID, callback, token);
		}

		public void CreateLinkInFolder<T>(Guid objectGUID, uint folderID, Action<bool, T> callback = null, T token = default(T))
		{
			_client.Link.Create(objectGUID, folderID).Callback = (result, error, t) =>
																{
																	if (callback != null)
																		callback(error == null, token);
																};
		}

		#endregion
		#region GetResult

		private IManagerResult<Object> GetResult(string query, string sort, uint pageSize, bool includeFiles, bool includeMetadata, bool includeObjectRelations, bool includeAccessPoints)
		{
			return new ManagerResult<Object>(pageSize, (i, r) =>
			{
				var state = _client.Object.Get(query, sort, (int)i, (int)pageSize, includeMetadata, includeFiles, includeObjectRelations, includeAccessPoints);
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

			if(@object.Metadatas.Any(m => m.MetadataSchemaGUID == schema.GUID && m.LanguageCode == language.LanguageCode))
				throw new Exception(string.Format("Object already have metadata with {0} language", language.LanguageCode));

			return AddLanguage(@object, schema.GUID, language.LanguageCode);
		}

		public Metadata AddLanguage(Object @object, Guid schemaGUID, string languageCode)
		{
			var metadata = new Metadata
			{
				MetadataSchemaGUID = schemaGUID,
				LanguageCode = languageCode
			};

			if(@object.Metadatas == null)
				@object.Metadatas = new ObservableCollection<Metadata>();

			@object.Metadatas.Add(metadata);

			return metadata;
		}

		#endregion
		#region SaveMetadata

		public void SaveMetadata(Metadata metadata, XElement newData, Action<bool> callback = null)
		{
			SaveMetadata(metadata, newData, callback == null ? null : (Action<bool, object>)((s, t) => callback(s)));
		}

		public void SaveMetadata<T>(Metadata metadata, XElement newData, Action<bool, T> callback = null, T token = default(T))
		{
			metadata.ValidateIsNotNull("metadata");
			newData.ValidateIsNotNull("newData");

			var @object = _objects.Values.FirstOrDefault(o => o.Metadatas != null && o.Metadatas.Contains(metadata));

			if(@object == null)
				throw new Exception("Could not find object matching metadata");

			metadata.MetadataXML = newData;

			_client.Metadata.Set(@object.GUID, metadata.MetadataSchemaGUID, metadata.LanguageCode, metadata.RevisionID, newData).Callback = (result, error, o) =>
			                                                                                                                                	{
																																					if (callback != null)
																																						callback(error == null, token);
			                                                                                                                                	};
		}

		#endregion
		#region Update methods

		private IList<Object> UpdateObjects(IList<Object> objects)
		{
			var result = new List<Object>();

			foreach (var newObject in objects)
			{
				if (_objects.ContainsKey(newObject.GUID))
				{
					var existingObject = _objects[newObject.GUID];

					UpdateObject(existingObject, newObject);

					result.Add(existingObject);
				}
				else
				{
					_objects.Add(newObject.GUID, newObject);

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

			if (newObject.Metadatas != null)
			{
				if (oldObject.Metadatas == null)
					oldObject.Metadatas = newObject.Metadatas;
				else
					UpdateCollection(oldObject.Metadatas, newObject.Metadatas, (m1, m2) => m1.MetadataSchemaGUID == m2.MetadataSchemaGUID && m1.LanguageCode == m2.LanguageCode, UpdateMetadata);
			}

			if (newObject.Files != null)
			{
				if (oldObject.Files == null)
					oldObject.Files = newObject.Files;
				else
					UpdateCollection(oldObject.Files, newObject.Files, (f1, f2) => f1.URL == f2.URL, UpdateFile);
			}

			if (newObject.ObjectRelations != null)
			{
				if (oldObject.ObjectRelations == null)
					oldObject.ObjectRelations = newObject.ObjectRelations;
				else
					UpdateCollection(oldObject.ObjectRelations, newObject.ObjectRelations, (r1, r2) => r1.Object1GUID == r2.Object1GUID && r1.Object2GUID == r2.Object2GUID && r1.ObjectRelationTypeID == r2.ObjectRelationTypeID, UpdateObjectRelation);
			}
		}

		private static void UpdateCollection<T>(ObservableCollection<T> oldCollection, ObservableCollection<T> newCollection, Func<T, T, bool> comparer, Action<T, T> updater) where T : class
		{
			if (newCollection == null)
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
			oldRelation.Sequence = newRelation.Sequence;
		}
		
		#endregion
	}
}