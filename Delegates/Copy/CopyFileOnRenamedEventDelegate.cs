using System.IO;
using File_Watcher.Delegates.Interfaces.Copy;
using File_Watcher.Services.Interfaces;

namespace File_Watcher.Delegates.Copy
{
    public class CopyFileOnRenamedEventDelegate : ICopyFileOnRenamedEventDelegate
    {
        private readonly IFileCopyService fileCopyService;

        public CopyFileOnRenamedEventDelegate(IFileCopyService fileCopyService)
        {
            this.fileCopyService = fileCopyService;
        }
        
        public void OnRenamed(object source, FileSystemEventArgs e)
        {
            fileCopyService.Copy(e, TargetPath);
        }
        
        public DirectoryInfo TargetPath { get; set; }
    }
}