using System.IO;

namespace File_Watcher.Delegates.Interfaces
{
    public interface IOnRenamedEventDelegate
    {
        public void OnRenamed(object source, FileSystemEventArgs e);
    }
}