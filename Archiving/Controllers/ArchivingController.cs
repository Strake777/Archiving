using Microsoft.AspNetCore.Mvc;
using Archiving.BLL.Interfaces;
using Archiving.Models;
using Archiving.BLL.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Archiving.Controllers
{
    public class ArchivingController : Controller
    {
        private readonly IArchivingService _archivingService;
        private readonly IHubContext<ArchivingHub> _hubContext;
        private readonly ILogger<IArchivingService> _logger;


        public ArchivingController(IArchivingService archivingService, IHubContext<ArchivingHub> hubContext, ILogger<IArchivingService> logger)
        {
            _archivingService = archivingService;
            _hubContext = hubContext;
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IActionResult ArchivingFiles(string directoryName)
        {
            var directoryModel = new DirectoryModel(directoryName, _archivingService.GetFiles(directoryName));
            return View(directoryModel);
        }

        [Authorize]
        [HttpPost]      
        public IActionResult StartArchive(DirectoryModel directoryModel)
        {
            directoryModel.FilesInfo = _archivingService.GetFiles(directoryModel.DirectoryName);
            _archivingService.StartArchivingAsync(directoryModel.FilesInfo, _logger, _hubContext);
            return Ok("Запрос успешно выполнен");
        }

        [Authorize]
        [HttpGet]
        public IActionResult StopArchive()
        {
            _archivingService.StopArchiving();
            _logger.LogInformation("Cancel archiving");
            return Ok("Запрос успешно выполнен");
        }
    }
}