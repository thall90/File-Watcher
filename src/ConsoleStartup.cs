using System;
using FileWatcher.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcher
{
    public static class ConsoleStartup
    {
        public static IServiceProvider SetupDependencyInjection()
        {
            return new ServiceCollection()
                .RegisterBuilders()
                .RegisterCommandInitializers()
                .RegisterDelegates()
                .RegisterServices()
                .BuildServiceProvider(false);
        }
    }
}