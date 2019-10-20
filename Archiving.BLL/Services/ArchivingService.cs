using System.Collections.Generic;
using Archiving.BLL.Interfaces;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Archiving.BLL.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Archiving.BLL.Archiving;
using Archiving.BLL.Infrastructure;
using System.Linq;

namespace Archiving.BLL.Services
{
    public class ArchivingService : IArchivingService
    {
        private readonly Archiver _archiver;
        private List<Task> _taskList;
        private CancellationTokenSource _cancelTokenSource;

        public ArchivingService()
        {
            _archiver = new Archiver();
        }

        public List<FileInfo> GetFiles(string directoryName)
        {
            if (directoryName != null)
            {
                var directoryInfo = new DirectoryInfo(directoryName);
                if (directoryInfo.Exists)
                {
                    var files = new List<FileInfo>(directoryInfo.GetFiles());
                    return files;
                }
            }
            return null;
        }
        
        public async void StartArchivingAsync(List<FileInfo> fileInfos, ILogger<IArchivingService> logger, IHubContext<ArchivingHub> hubContext)
        {
            if (fileInfos == null) return;
            _cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = _cancelTokenSource.Token;
            _taskList = (from fileInfo in fileInfos
                         where fileInfo.Extension != ArchiveConfig.ArchiveExtension
                         select _archiver.CompressFileAsync(fileInfo, hubContext, logger, token))
                         .ToList();
            await Task.WhenAll(_taskList);
        }

        public void StopArchiving()
        {
            _cancelTokenSource.Cancel();
            _taskList.Clear();
        }
    }  
}
