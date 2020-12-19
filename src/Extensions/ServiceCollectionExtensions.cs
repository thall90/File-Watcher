using System.Diagnostics.CodeAnalysis;
using FileWatcher.Builders;
using FileWatcher.Builders.Interfaces;
using FileWatcher.CommandInitializers;
using FileWatcher.Commands.Interfaces;
using FileWatcher.Delegates.Copy;
using FileWatcher.Delegates.Interfaces.Copy;
using FileWatcher.Services;
using FileWatcher.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcher.Extensions
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
        
        public static IServiceCollection RegisterCommandInitializers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ICopyCommandInitializer, CopyCommandInitializer>();
            return serviceCollection;
        }
    }
}