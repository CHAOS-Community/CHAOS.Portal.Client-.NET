using System;
using System.Xml.Linq;
using CHAOS.Portal.Client.Data;
using CHAOS.Portal.Client.Data.MCM;
using CHAOS.Portal.Client.Extensions;
using CHAOS.Portal.Client.ServiceCall;
using CHAOS.Portal.Client.Standard.ServiceCall;
using CHAOS.Web;

namespace CHAOS.Portal.Client.Standard.Extension
{
    public class FileExtension : AExtension, IFileExtension
    {
        public FileExtension(IServiceCaller serviceCaller) : base(serviceCaller) { }

        public IServiceCallState<IServiceResult_MCM<File>> Create(Guid objectGUID, uint? parentFileID, uint formatID, uint destinationID, string filename, string originalFilename, string folderPath)
        {
            return CallService<IServiceResult_MCM<File>>(HTTPMethod.GET, objectGUID, parentFileID, formatID, destinationID, filename, originalFilename, folderPath);
        }
    }
}
