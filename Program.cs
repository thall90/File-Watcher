using System;
using System.Diagnostics;
using System.IO;
using File_Watcher.Builders.Interfaces;
using File_Watcher.Delegates.Interfaces.Copy;
using File_Watcher.Extensions;
using File_Watcher.Services;
using Microsoft.Extensions.DependencyInjection;

namespace File_Watcher
{
    class Program
    {
        private static IServiceProvider ServiceProvider { get; set; }
        
        static void Main(DirectoryInfo source, DirectoryInfo target)
        {
            const string consoleAppOperation = "File System Watcher";
            var watch = Stopwatch.StartNew();
            var exitCode = 0;

            ConsolePrintingExtensions.PrintStartMessage(consoleAppOperation);

            ServiceProvider = ConsoleStartup.SetupDependencyInjection();

            try
            {
                using (var scope = ServiceProvider.CreateScope())
                {
                    var scopedProvider = scope.ServiceProvider;
                    var fileWatcherService = GetFileWatcherService(scopedProvider, target);
            
                    fileWatcherService.Watch(source);
                }
            }
            catch (Exception e)
            {
                ConsolePrintingExtensions.PrintError($"\n {e} \n");
                exitCode = -1;
            }
            finally
            {
                watch.Stop();

                ConsolePrintingExtensions.PrintExitMessage(consoleAppOperation, exitCode, watch);
            }
        }

        private static IFileWatcherService GetFileWatcherService(
            IServiceProvider serviceProvider,
            DirectoryInfo target)
        {
            var fileSystemWatcherBuilder = serviceProvider.GetService<IFileSystemWatcherBuilder>();
            var (copyFileOnChangedDelegate, copyFileOnCreatedDelegate, copyFileOnRenamedDelegate) = GetCopierDelegates(serviceProvider, target);

            var fileWatcherService = new FileWatcherService(
                copyFileOnChangedDelegate,
                copyFileOnCreatedDelegate,
                copyFileOnRenamedDelegate,
                fileSystemWatcherBuilder);

            return fileWatcherService;
        }

        private static (ICopyFileOnChangedEventDelegate, ICopyFileOnCreatedEventDelegate, ICopyFileOnRenamedEventDelegate) GetCopierDelegates(
            IServiceProvider serviceProvider,
            DirectoryInfo target)
        {
            var copyFileOnChangedDelegate = serviceProvider.GetRequiredService<ICopyFileOnChangedEventDelegate>();
            var copyFileOnCreatedDelegate = serviceProvider.GetRequiredService<ICopyFileOnCreatedEventDelegate>();
            var copyFileOnRenamedDelegate = serviceProvider.GetRequiredService<ICopyFileOnRenamedEventDelegate>();

            copyFileOnChangedDelegate.TargetPath = target;
            copyFileOnCreatedDelegate.TargetPath = target;
            copyFileOnRenamedDelegate.TargetPath = target;

            return (copyFileOnChangedDelegate, copyFileOnCreatedDelegate, copyFileOnRenamedDelegate);
        }
    }
}