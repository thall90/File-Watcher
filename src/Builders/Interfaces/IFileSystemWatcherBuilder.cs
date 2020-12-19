using System.IO;

namespace FileWatcher.Builders.Interfaces
{
    public interface IFileSystemWatcherBuilder
    {
        IFileSystemWatcherBuilder WithPath(DirectoryInfo directoryInfo);
        
        IFileSystemWatcherBuilder WithNotifyFilter(NotifyFilters notifyFilters);
        
        IFileSystemWatcherBuilder WithFilter(string filter);

        IFileSystemWatcherBuilder WithFilters(string[] filters);
        
        FileSystemWatcher Build();
    }
}