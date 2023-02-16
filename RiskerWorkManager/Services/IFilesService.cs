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
    }
}
