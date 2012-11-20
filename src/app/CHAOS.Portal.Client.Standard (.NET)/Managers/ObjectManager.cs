using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using CHAOS.Events;
using CHAOS.Portal.Client.Managers.Data;
using CHAOS.Tasks;
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

		private readonly IPortalClient _client;

		private readonly IDictionary<Guid, Object> _objects;
		private readonly IDictionary<Object, DeferredTasksInvoker> _clientSideOnlyObjects;

		public ObjectManager(IPortalClient client)
		{
			_client = ArgumentUtilities.ValidateIsNotNull("client", client);
			_objects = new Dictionary<Guid, Object>();
			_clientSideOnlyObjects = new Dictionary<Object, DeferredTasksInvoker>();
		}

		#region Create

		public Object Create<T>(uint objectTypeID, uint folderID, Guid? guid, Action<bool, T> callback, T token)
		{
			return Create(objectTypeID, folderID, guid, callback == null ? null : (Action<bool>)(s => callback(s, token)));
		}

		public Object Create(uint objectTypeID, uint folderID, Guid? guid = null, Action<bool> callback = null)
		{
			var @object = new Object();

			if (guid.HasValue)
			{
				@object.GUID = guid.Value;
				_objects[guid.Value] = @object;
			}

			_client.Object.Create(guid, objectTypeID, folderID).WithCallback(CreateCompleted, new CallbackToken<Object>(@object, callback));

			return @object;
		}

		public Object CreateClientSideObject(uint objectTypeID, uint folderID, Guid? guid)
		{
			var @object = new Object();

			if(!guid.HasValue)
				guid = Guid.NewGuid();

			@object.GUID = guid.Value;
			_objects[guid.Value] = @object;

			AddClientSideOnlyObject(@object);
			RunActionOnObject(@object, c => _client.Object.Create(guid, objectTypeID, folderID).WithCallback(CreateCompleted, new CallbackToken<Object>(@object, c)));

			return @object;
		}

		private void CreateCompleted(IServiceResult_MCM<Object> result, Exception error, object token)
		{
			var callbackToken = (CallbackToken<Object>)token;

			if(error == null && result.MCM.Data.Count == 1)
			{
				UpdateObject(callbackToken.Token, result.MCM.Data[0]);

				if (!_objects.ContainsKey(callbackToken.Token.GUID))
					_objects[callbackToken.Token.GUID] = callbackToken.Token;

				callbackToken.CallCallback(true);
			}
			else
			{
				if (!_objects.ContainsKey(callbackToken.Token.GUID))
					_objects.Remove(callbackToken.Token.GUID);

				callbackToken.CallCallback(false);
			}	
		}

		#endregion
		#region Delete

		public void Delete<T>(Object @object, Action<bool, T> callback, T token)
		{
			Delete(@object.ValidateIsNotNull("@object").GUID, callback == null ? null : (Action<bool>) (s => callback(s, token)));
		}

		public void Delete<T>(Guid objectGUID, Action<bool, T> callback, T token)
		{
			Delete(objectGUID, callback == null ? null : (Action<bool>)(s => callback(s, token)));
		}

		public void Delete(Object @object, Action<bool> callback = null)
		{
			Delete(@object.ValidateIsNotNull("@object").GUID, callback);
		}

		public void Delete(Guid objectGUID, Action<bool> callback = null)
		{
			RemoveRelations(objectGUID);

			if (IsClientSideOnlyObject(objectGUID))
				RemoveClientOnlyObject(_objects[objectGUID], true);
			else
			{
				_client.Object.Delete(objectGUID).WithCallback((result, error, o) =>
				{
					if (error == null)
					{
						_objects.Remove(objectGUID);
						RemoveRelations(objectGUID);

						if (callback != null)
							callback(true);
					}
					else if (callback != null) 
						callback(false);
				});
			}
		}

		private void RemoveRelations(Guid objectGUID)
		{
			if (_objects.ContainsKey(objectGUID))
				RemoveRelations(_objects[objectGUID]);
			else
				foreach (var @object in _objects.Values.Where(@object => @object.GUID != objectGUID))
					RemoveRelations(@object, objectGUID);
		}

		private void RemoveRelations(Object @object)
		{
			if (@object.ObjectRelations == null) return;
			
			foreach (var relation in @object.ObjectRelations)
			{
				var relatedObjectGUID = relation.Object1GUID == @object.GUID ? relation.Object2GUID : relation.Object1GUID;
				
				if (!_objects.ContainsKey(relatedObjectGUID)) continue;
				
				var relatedObject = _objects[relatedObjectGUID];

				if (relatedObject.ObjectRelations == null) continue;

				if (relatedObject.ObjectRelations.Contains(relation))
					relatedObject.ObjectRelations.Remove(relation);
				else
					RemoveRelations(relatedObject, @object.GUID);
			}

			@object.ObjectRelations.Clear();
		}

		private void RemoveRelations(Object @object, Guid relatedObjectGUID)
		{
			if (@object.ObjectRelations == null) return;

			foreach (var relation in @object.ObjectRelations.Where(r => r.Object1GUID == relatedObjectGUID || r.Object2GUID == relatedObjectGUID).ToList())
				@object.ObjectRelations.Remove(relation);
		}

		#endregion
		#region ObjectRelation

		public void CreateRelation<T>(Object object1, Object object2, ObjectRelationType relationType, int? sequence, Action<bool, T> callback, T token)
		{
			CreateRelation(object1.ValidateIsNotNull("object1").GUID, object2.ValidateIsNotNull("object2").GUID, relationType.ValidateIsNotNull("relationType").ID, sequence, callback == null ? null : (Action<bool>)(s => callback(s, token)));
		}

		public void CreateRelation(Object object1, Object object2, ObjectRelationType relationType, int? sequence, Action<bool> callback = null)
		{
			CreateRelation(object1.ValidateIsNotNull("object1").GUID, object2.ValidateIsNotNull("object2").GUID, relationType.ValidateIsNotNull("relationType").ID, sequence, callback);
		}

		public void CreateRelation<T>(Guid object1GUID, Guid object2GUID, uint relationTypeID, int? sequence, Action<bool, T> callback, T token)
		{
			CreateRelation(object1GUID, object2GUID, relationTypeID, sequence, callback == null ? null : (Action<bool>)(s => callback(s, token)));
		}

		public void CreateRelation(Guid object1GUID, Guid object2GUID, uint relationTypeID, int? sequence, Action<bool> callback = null)
		{
			var relation = new ObjectRelation(object1GUID, object2GUID, relationTypeID, sequence);

			AddRelationIfCached(object1GUID, relation);
			AddRelationIfCached(object2GUID, relation);

			if(IsClientSideOnlyObject(object1GUID) && IsClientSideOnlyObject(object2GUID))
				throw new NotImplementedException("Both objects can't be client side only");

			var @object = IsClientSideOnlyObject(object1GUID) ? _objects[object1GUID] : IsClientSideOnlyObject(object2GUID) ? _objects[object2GUID] : null;

			Action<Action<bool>> action = a => _client.ObjectRelation.Create(object1GUID, object2GUID, relationTypeID, sequence).Callback = (result, error, token) => a(error == null && result.MCM.Data.Count == 1);

			if (@object == null)
				action(callback);
			else
				RunActionOnObject(@object, action, callback);
		}

		private void AddRelationIfCached(Guid guid, ObjectRelation relation)
		{
			if (!_objects.ContainsKey(guid)) return;

			var @cachedObject = _objects[guid];

			if (cachedObject.ObjectRelations == null)
				cachedObject.ObjectRelations = new ObservableCollection<ObjectRelation>();

			cachedObject.ObjectRelations.Add(relation);
		}

		#endregion
		#region By GUID

		public Object GetObjectByGUID(Guid guid, bool includeFiles, bool includeMetadata, bool includeObjectRelations, bool includeAccessPoints)
		{
			if (!_objects.ContainsKey(guid))
				_objects[guid] = new Object {GUID = guid};
				
			var result = _objects[guid];

			if(!IsClientSideOnlyObject(result))
				_client.Object.Get(string.Format("GUID:{0}", guid), null, 0, 1, includeMetadata, includeFiles, includeObjectRelations, includeAccessPoints).Callback = GetObjectByGUIDCompleted;

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
			var @object = _objects.Values.FirstOrDefault(o => o.Metadatas != null && o.Metadatas.Contains(metadata));

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

		public IManagerResult<Object> GetObjectsByFolder(Folder folder, string sort, uint pageSize, bool includeFiles, bool includeMetadata, bool includeObjectRelations, bool includeAccessPoints)
		{
			return GetObjectsByFolder(folder.ValidateIsNotNull("folder").ID, sort, pageSize, includeFiles, includeMetadata, includeObjectRelations, includeAccessPoints);
		}

		public IManagerResult<Object> GetObjectsByFolder(uint folderID, string sort, uint pageSize, bool includeFiles, bool includeMetadata, bool includeObjectRelations, bool includeAccessPoints)
		{
			return GetResult(string.Format("FolderID:{0}", folderID), sort, pageSize, includeFiles, includeMetadata, includeObjectRelations, includeAccessPoints);
		}

		#endregion
		#region Link

		public void MoveLinkToFolder<T>(Object @object, Folder fromFolder, Folder toFolder, Action<bool, T> callback, T token)
		{
			MoveLinkToFolder(@object, fromFolder, toFolder, r => callback(r, token));
		}

		public void MoveLinkToFolder(Object @object, Folder fromFolder, Folder toFolder, Action<bool> callback = null)
		{
			MoveLinkToFolder(@object.GUID, fromFolder.ID, toFolder.ID, callback);
		}

		public void MoveLinkToFolder<T>(Guid objectGUID, uint fromFolderID, uint toFolderID, Action<bool, T> callback, T token)
		{
			MoveLinkToFolder(objectGUID, fromFolderID, toFolderID, r => callback(r, token));
		}

		public void MoveLinkToFolder(Guid objectGUID, uint fromFolderID, uint toFolderID, Action<bool> callback = null)
		{
			_client.Link.Update(objectGUID, fromFolderID, toFolderID).Callback = (result, error, t) =>
			{
				if (callback != null)
					callback(error == null);
			};
		}

		
		public void CreateLinkInFolder<T>(Object @object, Folder folder, Action<bool, T> callback, T token)
		{
			CreateLinkInFolder(@object, folder, r => callback(r, token));
		}

		public void CreateLinkInFolder(Object @object, Folder folder, Action<bool> callback)
		{
			CreateLinkInFolder(@object.GUID, folder.ID, callback);
		}

		public void CreateLinkInFolder<T>(Guid objectGUID, uint folderID, Action<bool, T> callback, T token)
		{
			CreateLinkInFolder(objectGUID, folderID, r => callback(r, token));
		}

		public void CreateLinkInFolder(Guid objectGUID, uint folderID, Action<bool> callback)
		{
			_client.Link.Create(objectGUID, folderID).Callback = (result, error, t) =>
			{
				if (callback != null)
					callback(error == null);
			};
		}


		public void DeleteLinkFromFolder(Object @object, Folder folder, Action<bool> callback)
		{
			DeleteLinkFromFolder(@object.GUID, folder.ID, callback);
		}

		public void DeleteLinkFromFolder<T>(Object @object, Folder folder, Action<bool, T> callback, T token)
		{
			DeleteLinkFromFolder(@object, folder, r => callback(r, token));
		}

		public void DeleteLinkFromFolder(Guid objectGUID, uint folderID, Action<bool> callback)
		{
			_client.Link.Delete(objectGUID, folderID).Callback = (result, error, t) =>
			{
				if (callback != null)
					callback(error == null);
			};
		}

		public void DeleteLinkFromFolder<T>(Guid objectGUID, uint folderID, Action<bool, T> callback, T token)
		{
			DeleteLinkFromFolder(objectGUID, folderID, r => callback(r, token));
		}

		public void DeleteLinksFromFolder(IEnumerable<Object> objects, Folder folder, Action<bool> callback)
		{
			DeleteLinksFromFolder(objects.Select(o => o.GUID), folder.ID, callback);
		}

		public void DeleteLinksFromFolder<T>(IEnumerable<Object> objects, Folder folder, Action<bool, T> callback, T token)
		{
			DeleteLinksFromFolder(objects, folder, r => callback(r, token));
		}

		public void DeleteLinksFromFolder(IEnumerable<Guid> objectGUIDs, uint folderID, Action<bool> callback)
		{
			var allCallsSucceeded = true;
			var allCallsStarted = false;
			var activeCalls = 0;


			foreach (var objectGuiD in objectGUIDs)
			{
				activeCalls++;
				DeleteLinkFromFolder(objectGuiD, folderID, r =>
					                                           {
																   if (!r)
																	   allCallsSucceeded = false;

																   if (--activeCalls == 0 && allCallsStarted)
																	   callback(allCallsSucceeded);
					                                           });
				
			}

			allCallsStarted = true;
		}

		public void DeleteLinksFromFolder<T>(IEnumerable<Guid> objectGUIDs, uint folderID, Action<bool, T> callback, T token)
		{
			DeleteLinksFromFolder(objectGUIDs, folderID, r => callback(r, token));
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

			if(@object.Metadatas != null && @object.Metadatas.Any(m => m.MetadataSchemaGUID == schema.GUID && m.LanguageCode == language.LanguageCode))
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

			AddMetadataToObject(@object, metadata);

			return metadata;
		}

		#endregion
		#region SaveMetadata

		public void SaveMetadata<T>(Metadata metadata, XElement newData, Action<bool, T> callback, T token)
		{
			SaveMetadata(metadata, newData, callback == null ? null : (Action<bool>) (s => callback(s, token)));
		}

		public void SaveMetadata(Metadata metadata, XElement newData, Action<bool> callback = null)
		{
			metadata.ValidateIsNotNull("metadata");
			newData.ValidateIsNotNull("newData");

			var @object = _objects.Values.FirstOrDefault(o => o.Metadatas != null && o.Metadatas.Contains(metadata));

			if (@object == null)
				throw new Exception("Could not find object matching metadata");

			metadata.MetadataXML = newData;

			SaveMetadata(@object, metadata, callback);
		}

		public void SaveMetadata<T>(Object @object, Metadata metadata, Action<bool, T> callback, T token)
		{
			SaveMetadata(@object, metadata, callback == null ? null : (Action<bool>)(s => callback(s, token)));
		}

		public void SaveMetadata(Object @object, Metadata metadata, Action<bool> callback = null)
		{
			@object.ValidateIsNotNull("@object");
			metadata.ValidateIsNotNull("metadata");

			var cachedObject = _objects.Values.FirstOrDefault(o => o.Metadatas != null && o.Metadatas.Contains(metadata));

			if (cachedObject != null && cachedObject != @object)
				throw new Exception("Metadata belongs to another object");

			var oldRevision = metadata.RevisionID;

			metadata.RevisionID = metadata.RevisionID == null ? 1 : metadata.RevisionID++;

			AddMetadataToObject(@object, metadata);

			RunActionOnObject(@object, a => _client.Metadata.Set(@object.GUID, metadata.MetadataSchemaGUID, metadata.LanguageCode, oldRevision, metadata.MetadataXML).Callback = (result, error, token) => a.DoIfIsNotNull(c => c(error == null)), callback);
		}

		private void AddMetadataToObject(Object @object, Metadata metadata)
		{
			if (@object.Metadatas == null)
				@object.Metadatas = new ObservableCollection<Metadata>();

			if (!@object.Metadatas.Contains(metadata))
				@object.Metadatas.Add(metadata);
		}

		#endregion
		#region Delete Metadata

		public void DeleteMetadata<T>(Metadata metadata, Action<bool, T> callback, T token)
		{
			DeleteMetadata(metadata, callback == null ? null : (Action<bool>)(s => callback(s, token)));
		}

		public void DeleteMetadata(Metadata metadata, Action<bool> callback = null)
		{
			metadata.ValidateIsNotNull("metadata");

			throw new NotImplementedException();
		}

		#endregion
		#region ClientSide Only

		public bool IsClientSideOnlyObject(Object @object)
		{
			return _clientSideOnlyObjects.ContainsKey(@object.ValidateIsNotNull("@object"));
		}

		public bool IsClientSideOnlyObject(Guid guid)
		{
			return _clientSideOnlyObjects.Keys.Any(o => o.GUID == guid);
		}

		private void AddClientSideOnlyObject(Object @object)
		{
			_clientSideOnlyObjects[@object] = new DeferredTasksInvoker();
		}

		private void RemoveClientOnlyObject(Object @object, bool deleteObject = false)
		{
			_clientSideOnlyObjects.Remove(@object);

			if(deleteObject)
			{
				_objects.Remove(@object.GUID);
				RemoveRelations(@object);
			}
		}

		private void RunActionOnObject(Object @object, Action<Action<bool>> action, Action<bool> callback = null)
		{
			if (IsClientSideOnlyObject(@object))
			{
				_clientSideOnlyObjects[@object].AddAction(action);
				if (callback != null)
					callback(true);
			}
			else
				action(callback);
		}

		public void SendClientSideOnlyObjectToServer<T>(Object @object, Action<bool, T> callback, T token)
		{
			SendClientSideOnlyObjectToServer(@object, callback == null ? null : (Action<bool>)(s => callback(s, token)));
		}

		public void SendClientSideOnlyObjectToServer(Object @object, Action<bool> callback)
		{			
			@object.ValidateIsNotNull("@object");

			DeferredTasksInvoker invoker;

			lock(_clientSideOnlyObjects)
			{
				if (!IsClientSideOnlyObject(@object))
				{
					if (_objects.ContainsKey(@object.GUID))
					{
						callback(true);
						return; //Assume object has been send to server
					}
						
					throw new Exception("ClientSide only object not found");
				}

				invoker = _clientSideOnlyObjects[@object];
				RemoveClientOnlyObject(@object); //TODO: Wait and remove invoker when all tasks are run, in case tasks are added in between now and then
			}

			invoker.Invoke(callback);
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

		private void UpdateObject(Object oldObject, Object newObject)
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
					UpdateCollection(oldObject.ObjectRelations, newObject.ObjectRelations, (r1, r2) => r1.Object1GUID == r2.Object1GUID && r1.Object2GUID == r2.Object2GUID && r1.ObjectRelationTypeID == r2.ObjectRelationTypeID, UpdateObjectRelation, oR => IsClientSideOnlyObject(oR.Object1GUID) || IsClientSideOnlyObject(oR.Object2GUID));
			}
		}

		private static void UpdateCollection<T>(ObservableCollection<T> oldCollection, ObservableCollection<T> newCollection, Func<T, T, bool> comparer, Action<T, T> updater, Func<T, bool> keepChecker = null) where T : class
		{
			if (newCollection == null)
				return;

			for (var i = 0; i < oldCollection.Count; i++)
			{
				var olditem = oldCollection[i];

				var newItem = newCollection.FirstOrDefault(item => comparer(olditem, item));

				if(newItem == null)
				{
					if (keepChecker != null && !keepChecker(olditem))
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
			oldMetadata.EditingUserGUID = newMetadata.EditingUserGUID;
			oldMetadata.RevisionID = newMetadata.RevisionID;
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