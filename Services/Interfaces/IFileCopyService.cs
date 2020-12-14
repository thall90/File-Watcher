using System.IO;

namespace File_Watcher.Services.Interfaces
{
    public interface IFileCopyService
    {
        void Copy(FileSystemEventArgs e, DirectoryInfo targetPath);

        void Copy(string sourceFile, string targetFile);
    }
}