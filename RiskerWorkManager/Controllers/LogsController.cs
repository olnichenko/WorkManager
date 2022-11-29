using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskerWorkManager.Attributes;
using RiskerWorkManager.Services;
using System.Text.Encodings.Web;
using System.Web;
using WorkManagerDal.Services;

namespace RiskerWorkManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogReaderService _logReaderService;
        public LogsController(ILogReaderService logReaderService)
        {
            _logReaderService = logReaderService;
        }

        [HttpGet]
        [AuthorizePermission(PermissionsService.Logs_View)]
        public string GetLogFilesList()
        {
            var result = _logReaderService.ReadFiles();
            return result;
        }
    }
}
