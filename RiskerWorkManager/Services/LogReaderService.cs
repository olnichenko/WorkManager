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
            // FileInfo fi = new FileInfo(@"D:\DummyFile.txt");

            //Open file for Read\Write
            FileStream fs = fileInfo.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);

            //Create object of StreamReader by passing FileStream object on which it needs to operates on
            StreamReader sr = new StreamReader(fs);

            //Use ReadToEnd method to read all the content from file
            string fileContent = sr.ReadToEnd();

            //Close StreamReader object after operation
            sr.Close();
            fs.Close();
            // var result = myFile.Re
            //var files = Directory.GetFiles(path);
            //return files;
            return fileContent;
        }
    }
}
