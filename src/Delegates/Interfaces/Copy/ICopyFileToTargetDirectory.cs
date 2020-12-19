using System.IO;

namespace FileWatcher.Delegates.Interfaces.Copy
{
    public interface ICopyFileToTargetDirectory
    {
        public DirectoryInfo? TargetPath { get; set; }
    }
}