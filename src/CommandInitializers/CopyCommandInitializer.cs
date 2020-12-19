using System;
using System.CommandLine;
using System.IO;
using FileWatcher.Builders.Interfaces;
using FileWatcher.Commands.Interfaces;
using FileWatcher.Delegates.Interfaces.Copy;
using FileWatcher.Extensions;
using FileWatcher.Services;
using FileWatcher.Services.Interfaces;

namespace FileWatcher.CommandInitializers
{
    public class CopyCommandInitializer : ICopyCommandInitializer
    {
        private readonly IFileSystemWatcherBuilder fileSystemWatcherBuilder;
        private readonly ICopyFileOnChangedEventDelegate copyFileOnChangedDelegate;
        private readonly ICopyFileOnCreatedEventDelegate copyFileOnCreatedDelegate;
        private readonly ICopyFileOnRenamedEventDelegate copyFileOnRenamedDelegate;

        public CopyCommandInitializer(
            IFileSystemWatcherBuilder fileSystemWatcherBuilder,
            ICopyFileOnChangedEventDelegate copyFileOnChangedDelegate,
            ICopyFileOnCreatedEventDelegate copyFileOnCreatedDelegate,
            ICopyFileOnRenamedEventDelegate copyFileOnRenamedDelegate)
        {
            this.fileSystemWatcherBuilder = fileSystemWatcherBuilder;
            this.copyFileOnChangedDelegate = copyFileOnChangedDelegate;
            this.copyFileOnCreatedDelegate = copyFileOnCreatedDelegate;
            this.copyFileOnRenamedDelegate = copyFileOnRenamedDelegate;
        }

        public Command Initialize()
        {
            var copyCommand = new Command("copyonchange")
            {
                new Argument<DirectoryInfo>("source", "The directory that files should be copied from"), 
                new Argument<DirectoryInfo>("target", "The directory that files should be copied to"), 
                new Option<string[]>(
                    "--filter",
                    description: "Comma-separated list of file extensions to filter by.",
                    getDefaultValue: Array.Empty<string>),
            };

            return copyCommand.WithHandler<CopyCommandInitializer>(nameof(HandleCopyCommand));
        }
        
        private void HandleCopyCommand(DirectoryInfo source, DirectoryInfo target, string[] filters)
        {
            var fileWatcherService = GetFileWatcherService(target, filters);
            
            fileWatcherService.Watch(source);
        }

        private IFileWatcherService GetFileWatcherService(
            DirectoryInfo target,
            string[] filters)
        {
            var (copyFileOnChanged, copyFileOnCreated, copyFileOnRenamed) = GetCopierDelegates(
                target);

            using (var fileSystemWatcher = CreateWatcher(target, filters))
            {
                var fileWatcherService = new FileWatcherService(
                    copyFileOnChanged,
                    copyFileOnCreated,
                    copyFileOnRenamed,
                    fileSystemWatcher);

                return fileWatcherService;   
            }
        }

        private (ICopyFileOnChangedEventDelegate, ICopyFileOnCreatedEventDelegate, ICopyFileOnRenamedEventDelegate) GetCopierDelegates(
            DirectoryInfo target)
        {
            copyFileOnChangedDelegate.TargetPath = target;
            copyFileOnCreatedDelegate.TargetPath = target;
            copyFileOnRenamedDelegate.TargetPath = target;

            return (copyFileOnChangedDelegate, copyFileOnCreatedDelegate, copyFileOnRenamedDelegate);
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