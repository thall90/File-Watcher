using System.IO;
using File_Watcher.Delegates.Interfaces.Copy;
using File_Watcher.Services.Interfaces;

namespace File_Watcher.Delegates.Copy
{
    public class CopyFileOnChangedEventDelegate : ICopyFileOnChangedEventDelegate
    {
        private readonly IFileCopyService fileCopyService;

        public CopyFileOnChangedEventDelegate(IFileCopyService fileCopyService)
        {
            this.fileCopyService = fileCopyService;
        }
        
        public void OnChanged(object source, FileSystemEventArgs e)
        {
            fileCopyService.Copy(e, TargetPath);
        }

        public DirectoryInfo TargetPath { get; set; }
    }
}