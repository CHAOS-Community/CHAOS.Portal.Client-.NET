﻿using System;
using CHAOS.Portal.Client.MCM.Data;
using CHAOS.Portal.Client.ServiceCall;

namespace CHAOS.Portal.Client.MCM.Extensions
{
    public interface IFileExtension
    {
        IServiceCallState<File> Create(Guid objectGUID, uint? parentFileID, uint formatID, uint destinationID, string filename, string originalFilename, string folderPath);
    }
}
