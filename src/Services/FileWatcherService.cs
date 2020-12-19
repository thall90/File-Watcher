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
        private readonly FileSystemWatcher fileSystemWatcher;

        public FileWatcherService(
            IOnChangedEventDelegate changedEventDelegate,
            IOnCreatedEventDelegate createdEventDelegate,
            IOnRenamedEventDelegate renamedEventDelegate,
            FileSystemWatcher fileSystemWatcher)
        {
            this.changedEventDelegate = changedEventDelegate;
            this.createdEventDelegate = createdEventDelegate;
            this.renamedEventDelegate = renamedEventDelegate;
            this.fileSystemWatcher = fileSystemWatcher;
        }

        public void Watch(DirectoryInfo directoryInfo)
        {
            fileSystemWatcher.Changed += changedEventDelegate.OnChanged;
            fileSystemWatcher.Created += createdEventDelegate.OnCreated;
            fileSystemWatcher.Deleted += changedEventDelegate.OnChanged;
            fileSystemWatcher.Renamed += renamedEventDelegate.OnRenamed;
        
            fileSystemWatcher.EnableRaisingEvents = true;
            
            Console.WriteLine(
                $"\n File watcher started for source directory ${directoryInfo.FullName}.\n" +
                "\n Press 'q' and then 'Enter' to stop.");
            
            while (Console.Read() != 'q')
            {
            }
        }
    }
}