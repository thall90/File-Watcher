using System.IO;

namespace File_Watcher.Delegates.Interfaces
{
    public interface IOnChangedEventDelegate
    {
        public void OnChanged(object source, FileSystemEventArgs e);
    }
}