using System.IO;

namespace FileWatcher.Delegates.Interfaces
{
    public interface IOnChangedEventDelegate
    {
        public void OnChanged(object source, FileSystemEventArgs eventArgs);
    }
}