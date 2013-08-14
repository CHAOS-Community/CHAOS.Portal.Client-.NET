using System;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
    public interface IFileExtension
    {
        IServiceCallState<PagedResult<File>> Create(Guid objectGuid, uint? parentFileID, uint formatID, uint destinationID, string filename, string originalFilename, string folderPath);
    }
}
