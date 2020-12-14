using System.IO;

namespace File_Watcher.Services
{
    public interface IFileWatcherService
    {
        void Watch(DirectoryInfo directoryInfo);
    }
}