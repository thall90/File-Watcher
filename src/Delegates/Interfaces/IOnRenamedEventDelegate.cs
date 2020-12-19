using System.IO;

namespace FileWatcher.Delegates.Interfaces
{
    public interface IOnRenamedEventDelegate
    {
        public void OnRenamed(object source, FileSystemEventArgs eventArgs);
    }
}