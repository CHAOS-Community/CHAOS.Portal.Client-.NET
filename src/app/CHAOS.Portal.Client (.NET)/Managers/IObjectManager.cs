using System;
using System.Collections.Generic;
using System.Xml.Linq;
using CHAOS.Events;
using CHAOS.Portal.Client.Data.MCM;
using Object = CHAOS.Portal.Client.Data.MCM.Object;

namespace CHAOS.Portal.Client.Managers
{
	public interface IObjectManager
	{
		event EventHandler<DataEventArgs<Exception>> FailedToGetObjectByGUID;
		event EventHandler<DataEventArgs<Exception>> FailedToGetObjects;

		Object Create(uint objectTypeID, uint folderID, Guid? guid = null, Action<bool> callback = null);
		Object Create<T>(uint objectTypeID, uint folderID, Guid? guid, Action<bool, T> callback, T token);

		Object CreateClientSideObject(uint objectTypeID, uint folderID, Guid? guid);

		void Delete(Object @object, Action<bool> callback = null);
		void Delete<T>(Object @object, Action<bool, T> callback, T token);
		void Delete(Guid objectGUID, Action<bool> callback = null);
		void Delete<T>(Guid objectGUID, Action<bool, T> callback, T token);

		void CreateRelation(Object object1, Object object2, ObjectRelationType relationType, int? sequence, Action<bool> callback = null);
		void CreateRelation<T>(Object object1, Object object2, ObjectRelationType relationType, int? sequence, Action<bool, T> callback, T token);
		void CreateRelation(Guid object1GUID, Guid object2GUID, uint relationTypeID, int? sequence, Action<bool> callback = null);
		void CreateRelation<T>(Guid object1GUID, Guid object2GUID, uint relationTypeID, int? sequence, Action<bool, T> callback, T token);

		Object GetObjectByGUID(Guid guid, bool includeFiles = false, bool includeMetadata = false, bool includeObjectRelations = false, bool includeAccessPoints = false);
		Object GetObjectByFileID(int fileID, bool includeFiles = false, bool includeMetadata = false, bool includeObjectRelations = false, bool includeAccessPoints = false);
		Object GetObjectByMetadata(Metadata metadata);

		IManagerResult<Object> GetObjectBySearch(string query, string sort, uint pageSize, bool includeFiles = false, bool includeMetadata = false, bool includeObjectRelations = false, bool includeAccessPoints = false);

		IManagerResult<Object> GetObjectsByFolder(Folder folder, string sort, uint pageSize, bool includeFiles = false, bool includeMetadata = false, bool includeObjectRelations = false, bool includeAccessPoints = false);
		IManagerResult<Object> GetObjectsByFolder(uint folderID, string sort, uint pageSize, bool includeFiles = false, bool includeMetadata = false, bool includeObjectRelations = false, bool includeAccessPoints = false);

		void MoveLinkToFolder(Object @object, Folder fromFolder, Folder toFolder, Action<bool> callback = null);
		void MoveLinkToFolder<T>(Object @object, Folder fromFolder, Folder toFolder, Action<bool, T> callback, T token);
		void MoveLinkToFolder(Guid objectGUID, uint fromFolderID, uint toFolderID, Action<bool> callback = null);
		void MoveLinkToFolder<T>(Guid objectGUID, uint fromFolderID, uint toFolderID, Action<bool, T> callback, T token);

		void CreateLinkInFolder(Object @object, Folder folder, Action<bool> callback = null);
		void CreateLinkInFolder<T>(Object @object, Folder folder, Action<bool, T> callback, T token);
		void CreateLinkInFolder(Guid objectGUID, uint folderID, Action<bool> callback = null);
		void CreateLinkInFolder<T>(Guid objectGUID, uint folderID, Action<bool, T> callback, T token);

		void DeleteLinkFromFolder(Object @object, Folder folder, Action<bool> callback = null);
		void DeleteLinkFromFolder<T>(Object @object, Folder folder, Action<bool, T> callback, T token);
		void DeleteLinkFromFolder(Guid objectGUID, uint folderID, Action<bool> callback = null);
		void DeleteLinkFromFolder<T>(Guid objectGUID, uint folderID, Action<bool, T> callback, T token);

		void DeleteLinksFromFolder(IEnumerable<Object> objects, Folder folder, Action<bool> callback = null);
		void DeleteLinksFromFolder<T>(IEnumerable<Object> objects, Folder folder, Action<bool, T> callback, T token);
		void DeleteLinksFromFolder(IEnumerable<Guid> objectGUIDs, uint folderID, Action<bool> callback = null);
		void DeleteLinksFromFolder<T>(IEnumerable<Guid> objectGUIDs, uint folderID, Action<bool, T> callback, T token);

		Metadata AddLanguage(Object @object, MetadataSchema schema, Language language);
		Metadata AddLanguage(Object @object, Guid schemaID, string languageCode);

		void SaveMetadata(Metadata metadata, XElement newData, Action<bool> callback = null);
		void SaveMetadata<T>(Metadata metadata, XElement newData, Action<bool, T> callback, T token);

		void SaveMetadata(Object @object, Metadata metadata, Action<bool> callback = null);
		void SaveMetadata<T>(Object @object, Metadata metadata, Action<bool, T> callback, T token);

		void DeleteMetadata(Metadata metadata, Action<bool> callback = null);
		void DeleteMetadata<T>(Metadata metadata, Action<bool, T> callback, T token);

		void SendClientSideOnlyObjectToServer(Object @object, Action<bool> callback = null);
		void SendClientSideOnlyObjectToServer<T>(Object @object, Action<bool, T> callback, T token);

		bool IsClientSideOnlyObject(Object @object);
		bool IsClientSideOnlyObject(Guid guid);
	}
}