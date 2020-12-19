using System.CommandLine;

namespace FileWatcher.Commands.Interfaces
{
    public interface ICopyCommandInitializer
    {
        Command Initialize();
    }
}