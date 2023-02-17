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
        public void RemoveFileFromComment(string fileName, long commentId)
        {
            _filesService.DeleteFileFromComment(fileName, commentId);
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
        public void RemoveFileFromBug(string fileName, long bugId)
        {
            _filesService.DeleteFileFromBug(fileName, bugId);
        }

        [HttpPost]
        [AuthorizePermission]
        public void RemoveFileFromNote(string fileName, long noteId)
        {
            _filesService.DeleteFileFromNote(fileName, noteId);
        }

        [HttpPost]
        [AuthorizePermission]
        public List<string> GetCommentFiles(long commentId)
        {
            if (commentId == 0)
            {
                return null;
            }
            return _filesService.GetCommentFileNames(commentId);
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
        [AuthorizePermission]
        public List<string> GetBugFiles(long bugId)
        {
            if (bugId == 0)
            {
                return null;
            }
            return _filesService.GetBugFileNames(bugId);
        }

        [HttpPost]
        [AuthorizePermission]
        public List<string> GetNoteFiles(long noteId)
        {
            if (noteId == 0)
            {
                return null;
            }
            return _filesService.GetNoteFileNames(noteId);
        }

        [HttpPost]
        public async Task<IActionResult> UploadToComment(long commentId)
        {
            var formCollection = await Request.ReadFormAsync();
            var files = formCollection.Files;

            await _filesService.SaveFilesToCommentAsync(files, commentId);

            return Ok();
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

        [HttpPost]
        public async Task<IActionResult> UploadToBug(long bugId)
        {
            var formCollection = await Request.ReadFormAsync();
            var files = formCollection.Files;

            await _filesService.SaveFilesToBugAsync(files, bugId);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UploadToNote(long noteId)
        {
            var formCollection = await Request.ReadFormAsync();
            var files = formCollection.Files;

            await _filesService.SaveFilesToNoteAsync(files, noteId);

            return Ok();
        }
    }
}
