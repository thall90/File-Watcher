using System;
using System.IO;
using System.IO.Abstractions;
using FileWatcher.Services.Interfaces;

namespace FileWatcher.Services
{
    public class FileCopyService : IFileCopyService
    {
        private readonly IFileSystem fileSystem;
        
        public FileCopyService() : this (new FileSystem()) {}

        public FileCopyService(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public void Copy(FileSystemEventArgs e, DirectoryInfo targetPath)
        {
            if (e?.Name == null)
            {
                return;
            }
            
            var targetFile = Path.Combine(targetPath.FullName, e.Name);

            Copy(e.FullPath, targetFile);
        }
        
        public void Copy(string sourceFile, string targetFile)
        {
            try
            {
                var targetDirectory = Path.GetDirectoryName(targetFile);
                
                if (!fileSystem.Directory.Exists(targetDirectory))
                {
                    fileSystem.Directory.CreateDirectory(targetDirectory);
                }
                
                if (fileSystem.File.Exists(targetFile))
                {
                    fileSystem.File.Delete(targetFile);
                }

                fileSystem.File.Copy(sourceFile, targetFile);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Failed to copy {sourceFile} to ${targetFile}. See exception: {exception}");
                throw;
            }
        }
    }
}