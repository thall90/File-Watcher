using System;
using System.IO;
using FileWatcher.Delegates.Interfaces.Copy;
using FileWatcher.Services.Interfaces;

namespace FileWatcher.Delegates.Copy
{
    public class CopyFileOnCreatedEventDelegate : ICopyFileOnCreatedEventDelegate
    {
        private readonly IFileCopyService fileCopyService;

        public CopyFileOnCreatedEventDelegate(IFileCopyService fileCopyService)
        {
            this.fileCopyService = fileCopyService;
        }

        public void OnCreated(object source, FileSystemEventArgs eventArgs)
        {
            if (TargetPath is null)
            {
                throw new Exception($"Cannot copy file without {nameof(TargetPath)}");
            }
            
            fileCopyService.Copy(eventArgs, TargetPath!);
        }

        public DirectoryInfo? TargetPath { get; set; }
    }
}