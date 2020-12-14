using System.IO;

namespace File_Watcher.Delegates.Interfaces.Copy
{
    public interface ICopyFileToTargetDirectory
    {
        public DirectoryInfo TargetPath { get; set; }
    }
}