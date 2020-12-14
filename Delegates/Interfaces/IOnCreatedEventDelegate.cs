using System.IO;

namespace File_Watcher.Delegates.Interfaces
{
    public interface IOnCreatedEventDelegate
    {
        void OnCreated(object source, FileSystemEventArgs e);
    }
}