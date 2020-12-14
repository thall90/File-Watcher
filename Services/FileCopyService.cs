using System;
using System.IO;
using File_Watcher.Services.Interfaces;

namespace File_Watcher.Services
{
    public class FileCopyService : IFileCopyService
    {
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
                if (File.Exists(targetFile))
                {
                    File.Delete(targetFile);
                }

                File.Move(sourceFile, targetFile);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Failed to copy {sourceFile} to ${targetFile}. See exception: {exception}");
            }
        }
    }
}