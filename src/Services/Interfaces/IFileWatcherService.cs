using System.IO;

namespace FileWatcher.Services.Interfaces
{
    public interface IFileWatcherService
    {
        void Watch(DirectoryInfo directoryInfo, string[] filters);
    }
}