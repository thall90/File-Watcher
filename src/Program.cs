using System;
using System.CommandLine;
using FileWatcher.Commands.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FileWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = ConsoleStartup.SetupDependencyInjection();

            using var scope = serviceProvider.CreateScope();
            var scopedServiceProvider = scope.ServiceProvider;

            var copyCommand = GetCopyCommand(scopedServiceProvider);
            var rootCommand = new RootCommand { copyCommand };

            rootCommand.Invoke(args);
        }

        private static Command GetCopyCommand(IServiceProvider serviceProvider)
        {
            var copyCommandInitializer = serviceProvider.GetRequiredService<ICopyCommandInitializer>();

            return copyCommandInitializer.Initialize();
        }
    }
}