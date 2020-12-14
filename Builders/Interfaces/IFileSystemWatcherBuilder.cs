using System.IO;

namespace File_Watcher.Builders.Interfaces
{
    public interface IFileSystemWatcherBuilder
    {
        IFileSystemWatcherBuilder WithPath(DirectoryInfo directoryInfo);
        
        IFileSystemWatcherBuilder WithNotifyFilter(NotifyFilters notifyFilters);
        
        IFileSystemWatcherBuilder WithFilter(string filter);
        
        FileSystemWatcher Build();
    }
}