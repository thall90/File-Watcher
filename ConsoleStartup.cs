using System;
using File_Watcher.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace File_Watcher
{
    public static class ConsoleStartup
    {
        public static IServiceProvider SetupDependencyInjection()
        {
            return new ServiceCollection()
                .RegisterBuilders()
                .RegisterDelegates()
                .RegisterServices()
                .BuildServiceProvider(false);
        }
    }
}