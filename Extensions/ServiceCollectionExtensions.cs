using System.Diagnostics.CodeAnalysis;
using File_Watcher.Builders;
using File_Watcher.Builders.Interfaces;
using File_Watcher.Delegates.Copy;
using File_Watcher.Delegates.Interfaces;
using File_Watcher.Delegates.Interfaces.Copy;
using File_Watcher.Services;
using File_Watcher.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace File_Watcher.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IFileWatcherService, FileWatcherService>();
            serviceCollection.AddTransient<IFileCopyService, FileCopyService>();
            return serviceCollection;
        }

        public static IServiceCollection RegisterDelegates(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ICopyFileOnChangedEventDelegate, CopyFileOnChangedEventDelegate>();
            serviceCollection.AddTransient<ICopyFileOnCreatedEventDelegate, CopyFileOnCreatedEventDelegate>();
            serviceCollection.AddTransient<ICopyFileOnRenamedEventDelegate, CopyFileOnRenamedEventDelegate>();
            return serviceCollection;
        }

        public static IServiceCollection RegisterBuilders(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IFileSystemWatcherBuilder, FileSystemWatcherBuilder>();
            return serviceCollection;
        }
    }
}