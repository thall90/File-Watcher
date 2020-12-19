using System.IO;
using FileWatcher.Builders.Interfaces;

namespace FileWatcher.Builders
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

        public IFileSystemWatcherBuilder WithFilters(string[] filters)
        {
            foreach (var filter in filters)
            {
                watcher.Filters.Add(filter);
            }

            return this;
        }

        public FileSystemWatcher Build()
        {
            return watcher;
        }
    }
}