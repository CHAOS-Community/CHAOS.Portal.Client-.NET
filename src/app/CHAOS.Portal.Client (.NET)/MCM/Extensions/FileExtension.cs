using System;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
    public class FileExtension : AExtension, IFileExtension
    {
        public IServiceCallState<File> Create(Guid objectGuid, uint? parentFileID, uint formatID, uint destinationID, string filename, string originalFilename, string folderPath)
        {
            return CallService<File>(HTTPMethod.GET, objectGuid, parentFileID, formatID, destinationID, filename, originalFilename, folderPath);
        }
    }
}
