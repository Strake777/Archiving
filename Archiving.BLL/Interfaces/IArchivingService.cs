using System.Collections.Generic;
using System.IO;
using Archiving.BLL.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Archiving.BLL.Interfaces
{
    public interface IArchivingService
    {
        List<FileInfo> GetFiles(string directoryName);
        void StartArchivingAsync(List<FileInfo> fileInfos, ILogger<IArchivingService> logger, IHubContext<ArchivingHub> hubContext);
        void StopArchiving();

    }
}
