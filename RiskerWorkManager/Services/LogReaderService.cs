using System.IO;

namespace RiskerWorkManager.Services
{
    public class LogReaderService : ILogReaderService
    {
        public string ReadFiles()
        {
            var path = AppContext.BaseDirectory;
            path += @"\logs\";
            var directory = new DirectoryInfo(path);
            
            var fileInfo = directory.GetFiles()
             .OrderByDescending(f => f.LastWriteTime)
             .First();
            using (var fs = fileInfo.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
            {
                using (var sr = new StreamReader(fs))
                {
                    var fileContent = sr.ReadToEnd();
                    return fileContent;
                }
            }
        }
    }
}
