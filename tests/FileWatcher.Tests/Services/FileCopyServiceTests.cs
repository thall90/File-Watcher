using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using FileWatcher.Services;
using FileWatcher.Tests.TestInfrastructure;
using FluentAssertions;
using Xunit;

namespace FileWatcher.Tests.Services
{
    public class FileCopyServiceTests
    {
        [Fact]
        public void Copy_Should_Copy_Source_File_To_Target_Location()
        {
            // Arrange
            const string sourceFilePath = "c:\\source\\sourceFile.txt";
            const string targetFilePath = "c:\\copy\\copiedFile.txt";
            
            var fileSystem = CreateFileSystem();
            AddDirectoryToFileSystem(fileSystem, targetFilePath);

            var sut = new FileCopyService(fileSystem);
            var sourceFile = fileSystem.GetFile(sourceFilePath);
            
            // Act
            sut.Copy(sourceFilePath, targetFilePath);
            
            var targetFile = fileSystem.GetFile(targetFilePath);

            // Assert
            targetFile.Should().BeEquivalentTo(sourceFile);
        }
        
        [Theory]
        [InlineData("c:\\source\\source.mp4", "test_mp4.mp4")]
        [InlineData("c:\\source\\source.mkv", "test_mkv.mkv")]
        public void Copy_Should_Handle_Copying_Multiple_Source_File_Types_To_Target_Location(
            string sourceFilePath,
            string embeddedResource)
        {
            // Arrange
            var targetFilePath = $"c:\\copy\\copiedFile.{Path.GetExtension(sourceFilePath)}";
            
            var fileSystem = CreateFileSystem();
            AddDirectoryToFileSystem(fileSystem, targetFilePath);
            AddEmbeddedResourceToFileSystem(fileSystem, sourceFilePath, embeddedResource);

            var sut = new FileCopyService(fileSystem);
            var sourceFile = fileSystem.GetFile(sourceFilePath);

            // Act
            sut.Copy(sourceFilePath, targetFilePath);
            
            var targetFile = fileSystem.GetFile(targetFilePath);

            // Assert
            targetFile.Should().BeEquivalentTo(sourceFile);
        }

        [Fact]
        public void Copy_Should_Create_Target_Directory_If_Not_Existing()
        {
            // Arrange
            const string sourceFileDirectory = "c:\\source\\sourceFile.txt";
            const string targetFileDirectory = "c:\\copy\\copiedFile.txt";
            
            var fileSystem = CreateFileSystem();

            /* Target Directory not created ahead of time */
            
            var sut = new FileCopyService(fileSystem);
            
            // Act && Assert
            sut.Invoking(x => x.Copy(sourceFileDirectory, targetFileDirectory))
                .Should()
                .NotThrow<DirectoryNotFoundException>();
        }
        
        [Fact]
        public void Copy_Should_Delete_Existing_Target_File_From_Target_Location()
        {
            // Arrange
            const string sourceFileDirectory = "c:\\source\\sourceFile.txt";
            const string targetFileDirectory = "c:\\copy\\copiedFile.txt";
            const string originalTargetFileContents = "Existing target file";
            
            var fileSystem = CreateFileSystem();
            AddDirectoryToFileSystem(fileSystem, targetFileDirectory);
            fileSystem.AddFile(targetFileDirectory, new MockFileData(originalTargetFileContents));
            
            var sut = new FileCopyService(fileSystem);
            var sourceFile = fileSystem.GetFile(sourceFileDirectory);
            
            // Act
            sut.Copy(sourceFileDirectory, targetFileDirectory);
            
            var targetFile = fileSystem.GetFile(targetFileDirectory);

            // Assert
            targetFile.TextContents.Should().NotBe(originalTargetFileContents);
            targetFile.Should().BeEquivalentTo(sourceFile);

        }
        
        private static MockFileSystem CreateFileSystem(
            string? sourceFileDirectory = null,
            MockFileData? sourceFileData = null)
        {
            var mockFiles = new Dictionary<string, MockFileData>
            {
                {
                    sourceFileDirectory ?? "c:\\source\\sourceFile.txt", 
                    sourceFileData ?? new MockFileData("Source file.")
                },
            };
            
            return new MockFileSystem(mockFiles);
        }

        private static void AddDirectoryToFileSystem(MockFileSystem fileSystem, string directory)
        {
            fileSystem.AddDirectory(Path.GetDirectoryName(directory));
        }
        
        private static void AddEmbeddedResourceToFileSystem(MockFileSystem fileSystem, string sourceFilePath,
            string embeddedResource)
        {
            var resource = new EmbeddedResourceReader().GetResource(embeddedResource);
            fileSystem.AddFile(sourceFilePath, resource);
        }
    }
}