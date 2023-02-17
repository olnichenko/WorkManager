namespace RiskerWorkManager.Services
{
    public interface IFilesService
    {
        Task SaveFilesToProjectAsync(IFormFileCollection files, long projectId);
        void DeleteFileFromProject(string fileName, long projectId);
        List<string> GetProjectFileNames(long projetId);
        Task SaveFilesToFeatureAsync(IFormFileCollection files, long featureId);
        List<string> GetFeatureFileNames(long featureId);
        void DeleteFileFromFeature(string fileName, long featureId);
        List<string> GetBugFileNames(long bugId);
        void DeleteFileFromBug(string fileName, long bugId);
        Task SaveFilesToBugAsync(IFormFileCollection files, long bugId);
        List<string> GetNoteFileNames(long noteId);
        void DeleteFileFromNote(string fileName, long noteId);
        void DeleteAllFilesFromNote(long noteId);
        Task SaveFilesToNoteAsync(IFormFileCollection files, long noteId);
        Task SaveFilesToCommentAsync(IFormFileCollection files, long commentId);
        void DeleteAllFilesFromComment(long commentId);
        void DeleteFileFromComment(string fileName, long commentId);
        List<string> GetCommentFileNames(long commentId);

    }
}
