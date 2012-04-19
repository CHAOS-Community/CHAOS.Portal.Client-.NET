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

		Object GetObjectByGUID(Guid guid, bool includeFiles, bool includeMetadata, bool includeObjectRelations);
		Object GetObjectByFileID(int fileID, bool includeFiles, bool includeMetadata, bool includeObjectRelations);

		IManagerResult<Object> GetObjectBySearch(string query, string sort, bool includeFiles, bool includeMetadata, bool includeObjectRelations, uint pageSize);

		IManagerResult<Object> GetObjectsByFolder(Folder folder, bool includeFiles, bool includeMetadata, bool includeObjectRelations, uint pageSize);
		IManagerResult<Object> GetObjectsByFolder(int folderID, bool includeFiles, bool includeMetadata, bool includeObjectRelations, uint pageSize);

		Metadata AddLanguage(Object @object, MetadataSchema schema, Language language);
		Metadata AddLanguage(Object @object, int schemaID, string languageCode);

		void SaveMetadata(Metadata metadata, XElement newData);
	}
}