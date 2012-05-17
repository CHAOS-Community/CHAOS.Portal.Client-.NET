using System;
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

		Object Create<T>(uint objectTypeID, uint folderID, Guid? guid = null, Action<bool, T> callback = null, T token = default(T));

		void CreateRelation<T>(Object object1, Object object2, ObjectRelationType relationType, int? sequence, Action<bool, T> callback = null, T token = default(T));
		void CreateRelation<T>(Guid object1, Guid object2, uint relationType, int? sequence, Action<bool, T> callback = null, T token = default(T));

		Object GetObjectByGUID(Guid guid, bool includeFiles, bool includeMetadata, bool includeObjectRelations);
		Object GetObjectByFileID(int fileID, bool includeFiles, bool includeMetadata, bool includeObjectRelations);

		IManagerResult<Object> GetObjectBySearch(string query, string sort, bool includeFiles, bool includeMetadata, bool includeObjectRelations, uint pageSize);
			
		IManagerResult<Object> GetObjectsByFolder(Folder folder, bool includeFiles, bool includeMetadata, bool includeObjectRelations, uint pageSize);
		IManagerResult<Object> GetObjectsByFolder(uint folderID, bool includeFiles, bool includeMetadata, bool includeObjectRelations, uint pageSize);

		void MoveLinkToFolder<T>(Object @object, Folder fromFolder, Folder toFolder, Action<bool, T> callback = null, T token = default(T));
		void MoveLinkToFolder<T>(Guid objectGUID, uint fromFolderID, uint toFolderID, Action<bool, T> callback = null, T token = default(T));

		void CreateLinkInFolder<T>(Object @object, Folder folder, Action<bool, T> callback = null, T token = default(T));
		void CreateLinkInFolder<T>(Guid objectGUID, uint folderID, Action<bool, T> callback = null, T token = default(T));

		Metadata AddLanguage(Object @object, MetadataSchema schema, Language language);
		Metadata AddLanguage(Object @object, Guid schemaID, string languageCode);

		void SaveMetadata(Metadata metadata, XElement newData);
	}
}