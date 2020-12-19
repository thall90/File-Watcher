using System.IO;

namespace FileWatcher.Services.Interfaces
{
    public interface IFileCopyService
    {
        void Copy(FileSystemEventArgs e, DirectoryInfo targetPath);

        void Copy(string sourceFile, string targetFile);
    }
}