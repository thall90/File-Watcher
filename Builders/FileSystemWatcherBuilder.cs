using System.IO;
using File_Watcher.Builders.Interfaces;

namespace File_Watcher.Builders
{
    public class FileSystemWatcherBuilder : IFileSystemWatcherBuilder
    {
        private readonly FileSystemWatcher watcher;

        public FileSystemWatcherBuilder()
        {
            watcher = new FileSystemWatcher();
        }

        public IFileSystemWatcherBuilder WithPath(DirectoryInfo directoryInfo)
        {
            watcher.Path = directoryInfo.FullName;
            return this;
        }

        public IFileSystemWatcherBuilder WithNotifyFilter(NotifyFilters notifyFilters)
        {
            watcher.NotifyFilter = notifyFilters;
            return this;
        }

        public IFileSystemWatcherBuilder WithFilter(string filter)
        {
            watcher.Filter = filter;
            return this;
        }

        public FileSystemWatcher Build()
        {
            return watcher;
        }
    }
}