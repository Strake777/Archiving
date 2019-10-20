using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;
using Archiving.BLL.Hubs;
using Microsoft.AspNetCore.SignalR;
using Archiving.BLL.Infrastructure;
using Microsoft.Extensions.Logging;
using Archiving.BLL.Interfaces;

namespace Archiving.BLL.Archiving
{
    public class Archiver
    {
        public async Task CompressFileAsync(FileInfo fileToCompress, IHubContext<ArchivingHub> hubContext, ILogger<IArchivingService> logger, CancellationToken token)
        {
            if ((File.GetAttributes(fileToCompress.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ArchiveConfig.ArchiveExtension)
            {
                logger.LogInformation("FILE {0} Started archiving", fileToCompress.FullName);
                using (FileStream originalFileStream = fileToCompress.OpenRead())
                using (FileStream compressedFileStream = File.Create(fileToCompress.FullName + ArchiveConfig.ArchiveExtension))
                using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionLevel.Fastest))
                {
                    byte[] buffer = new byte[fileToCompress.Length];
                    int i = originalFileStream.Read(buffer, 0, 10);
                    while (i > 0)
                    {
                        if (token.IsCancellationRequested)
                        {
                            logger.LogInformation("FILE {0} Canceled archiving", fileToCompress.FullName);
                            return;
                        }
                        long percent = (originalFileStream.Position * 100 / originalFileStream.Length);
                        await hubContext.Clients.All.SendAsync("Notify", fileToCompress.Name, percent, token);
                        compressionStream.Write(buffer, 0, i);
                        i = originalFileStream.Read(buffer, 0, 10);
                    }

                }
            }

        }
    }
}
