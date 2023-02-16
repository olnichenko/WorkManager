using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskerWorkManager.Attributes;
using RiskerWorkManager.Services;

namespace RiskerWorkManager.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        IFilesService _filesService;
        public FilesController(IFilesService filesService)
        {
            _filesService = filesService;
        }
        [HttpPost]
        [AuthorizePermission]
        public void RemoveFileFromFeature(string fileName, long featureId)
        {
            _filesService.DeleteFileFromFeature(fileName, featureId);
        }
        [HttpPost]
        [AuthorizePermission]
        public void RemoveFileFromProject(string fileName, long projectId)
        {
            _filesService.DeleteFileFromProject(fileName, projectId);
        }
        [HttpPost]
        [AuthorizePermission]
        public List<string> GetFeatureFiles(long featureId)
        {
            if (featureId == 0)
            {
                return null;
            }
            return _filesService.GetFeatureFileNames(featureId);
        }
        [HttpPost]
        [AuthorizePermission]
        public List<string> GetProjectFiles(long projectId)
        {
            if (projectId == 0)
            {
                return null;
            }
            return _filesService.GetProjectFileNames(projectId);
        }
        [HttpPost]
        public async Task<IActionResult> UploadToFeature(long featureId)
        {
            var formCollection = await Request.ReadFormAsync();
            var files = formCollection.Files;

            await _filesService.SaveFilesToFeatureAsync(files, featureId);

            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> UploadToProject(long projectId)
        {
            var formCollection = await Request.ReadFormAsync();
            var files = formCollection.Files;

            await _filesService.SaveFilesToProjectAsync(files, projectId);

            return Ok();
        }
    }
}
