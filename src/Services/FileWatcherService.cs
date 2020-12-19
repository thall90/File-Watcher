using System;
using System.IO;
using FileWatcher.Builders.Interfaces;
using FileWatcher.Delegates.Interfaces;
using FileWatcher.Services.Interfaces;

namespace FileWatcher.Services
{
    public class FileWatcherService : IFileWatcherService
    {
        private readonly IOnChangedEventDelegate changedEventDelegate;
        private readonly IOnCreatedEventDelegate createdEventDelegate;
        private readonly IOnRenamedEventDelegate renamedEventDelegate;
        private readonly IFileSystemWatcherBuilder fileSystemWatcherBuilder;

        public FileWatcherService(
            IOnChangedEventDelegate changedEventDelegate,
            IOnCreatedEventDelegate createdEventDelegate,
            IOnRenamedEventDelegate renamedEventDelegate,
            IFileSystemWatcherBuilder fileSystemWatcherBuilder)
        {
            this.changedEventDelegate = changedEventDelegate;
            this.createdEventDelegate = createdEventDelegate;
            this.renamedEventDelegate = renamedEventDelegate;
            this.fileSystemWatcherBuilder = fileSystemWatcherBuilder;
        }

        public void Watch(DirectoryInfo directoryInfo, string[] filters)
        {
            using var watcher = CreateWatcher(directoryInfo, filters);
            
            watcher.Changed += changedEventDelegate.OnChanged;
            watcher.Created += createdEventDelegate.OnCreated;
            watcher.Deleted += changedEventDelegate.OnChanged;
            watcher.Renamed += renamedEventDelegate.OnRenamed;
        
            watcher.EnableRaisingEvents = true;
            
            Console.WriteLine(
                $"\n File watcher started for source directory ${directoryInfo.FullName}.\n" +
                "\n Press 'q' and then 'Enter' to stop.");
            
            while (Console.Read() != 'q')
            {
            }
        }

        private FileSystemWatcher CreateWatcher(DirectoryInfo directoryInfo, string[] filters)
        {
            var watcher = fileSystemWatcherBuilder
                .WithFilters(filters)
                .WithPath(directoryInfo)
                .WithNotifyFilter(
                    NotifyFilters.LastAccess
                    | NotifyFilters.LastWrite
                    | NotifyFilters.FileName
                    | NotifyFilters.DirectoryName)
                .Build();

            return watcher;
        }
    }
}