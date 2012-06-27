using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.Extensions
{
    public interface IFileExtension
    {
        IServiceCallState<IServiceResult_MCM<File>> Create(Guid objectGUID, uint? parentFileID, uint formatID, uint destinationID, string filename, string originalFilename, string folderPath);
    }
}
