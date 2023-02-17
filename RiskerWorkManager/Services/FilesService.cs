using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using System.Security.AccessControl;

namespace RiskerWorkManager.Services
{
    public class FilesService : IFilesService
    {
        IWebHostEnvironment _appEnvironment;

        public FilesService(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }
        public List<string> GetFeatureFileNames(long featureId)
        {
            var path = GetFeatureDirectory(featureId);
            return GetDirectoryFileNames(path);
        }
        public List<string> GetProjectFileNames(long projetId)
        {
            var path = GetProjectDirectory(projetId);
            return GetDirectoryFileNames(path);
        }
        public List<string> GetBugFileNames(long bugId)
        {
            var path = GetBugDirectory(bugId);
            return GetDirectoryFileNames(path);
        }
        public List<string> GetNoteFileNames(long noteId)
        {
            var path = GetNoteDirectory(noteId);
            return GetDirectoryFileNames(path);
        }
        public List<string> GetCommentFileNames(long commentId)
        {
            var path = GetCommentDirectory(commentId);
            return GetDirectoryFileNames(path);
        }
        public void DeleteFileFromFeature(string fileName, long featureId)
        {
            var path = GetFeatureDirectory(featureId) + fileName;
            DeleteFile(path);
        }
        public void DeleteFileFromProject(string fileName, long projectId)
        {
            var path = GetProjectDirectory(projectId) + fileName;
            DeleteFile(path);
        }
        public void DeleteFileFromBug(string fileName, long bugId)
        {
            var path = GetBugDirectory(bugId) + fileName;
            DeleteFile(path);
        }
        public void DeleteFileFromNote(string fileName, long noteId)
        {
            var path = GetNoteDirectory(noteId) + fileName;
            DeleteFile(path);
        }
        public void DeleteAllFilesFromNote(long noteId)
        {
            var path = GetNoteDirectory(noteId);
            DeleteDirectory(path);
        }
        public void DeleteFileFromComment(string fileName, long commentId)
        {
            var path = GetCommentDirectory(commentId) + fileName;
            DeleteFile(path);
        }
        public void DeleteAllFilesFromComment(long commentId)
        {
            var path = GetCommentDirectory(commentId);
            DeleteDirectory(path);
        }
        public async Task SaveFilesToFeatureAsync(IFormFileCollection files, long featureId)
        {
            var path = GetFeatureDirectory(featureId);
            await SaveFilesAsync(files, path);
        }
        public async Task SaveFilesToProjectAsync(IFormFileCollection files, long projectId)
        {
            var path = GetProjectDirectory(projectId);
            await SaveFilesAsync(files, path);
        }
        public async Task SaveFilesToBugAsync(IFormFileCollection files, long bugId)
        {
            var path = GetBugDirectory(bugId);
            await SaveFilesAsync(files, path);
        }
        public async Task SaveFilesToNoteAsync(IFormFileCollection files, long noteId)
        {
            var path = GetNoteDirectory(noteId);
            await SaveFilesAsync(files, path);
        }
        public async Task SaveFilesToCommentAsync(IFormFileCollection files, long commentId)
        {
            var path = GetCommentDirectory(commentId);
            await SaveFilesAsync(files, path);
        }
        private string CommentsDirectoryPath
        {
            get
            {
                return _appEnvironment.WebRootPath + "/Files/Comments/";
            }
        }
        private string ProjectsDirectoryPath
        {
            get
            {
                return _appEnvironment.WebRootPath + "/Files/Projects/";
            }
        }

        private string FeaturesDirectoryPath
        {
            get
            {
                return _appEnvironment.WebRootPath + "/Files/Features/";
            }
        }

        private string BugsDirectoryPath
        {
            get
            {
                return _appEnvironment.WebRootPath + "/Files/Bugs/";
            }
        }

        private string NotesDirectoryPath
        {
            get
            {
                return _appEnvironment.WebRootPath + "/Files/Notes/";
            }
        }
        private string GetCommentDirectory(long commentId)
        {
            return CommentsDirectoryPath + commentId + "/";
        }
        private string GetProjectDirectory(long projectId)
        {
            return ProjectsDirectoryPath + projectId + "/";
        }

        private string GetFeatureDirectory(long featureId)
        {
            return FeaturesDirectoryPath + featureId + "/";
        }

        private string GetBugDirectory(long bugId)
        {
            return BugsDirectoryPath + bugId + "/";
        }

        private string GetNoteDirectory(long noteId)
        {
            return NotesDirectoryPath + noteId + "/";
        }

        private async Task SaveFilesAsync(IFormFileCollection files, string path)
        {
            foreach (var file in files)
            {
                await SaveFileAsync(file, path);
            }
        }

        private async Task SaveFileAsync(IFormFile file, string path)
        {
            try
            {
                if (file.Length > 0)
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    using (var fileStream = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("File save Failed", ex);
            }
        }

        private List<string> GetDirectoryFileNames(string path)
        {
            var directoryInfo = new DirectoryInfo(path);
            if (!directoryInfo.Exists)
            {
                return null;
            }
            
            var files = directoryInfo.GetFiles();
            var result = new List<string>();
            foreach (FileInfo file in files)
            {
                result.Add(file.Name);
            }
            return result;
        }

        private void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void DeleteDirectory(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }
    }
}
