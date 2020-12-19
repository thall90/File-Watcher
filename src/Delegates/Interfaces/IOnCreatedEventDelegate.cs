using System.IO;

namespace FileWatcher.Delegates.Interfaces
{
    public interface IOnCreatedEventDelegate
    {
        void OnCreated(object source, FileSystemEventArgs eventArgs);
    }
}