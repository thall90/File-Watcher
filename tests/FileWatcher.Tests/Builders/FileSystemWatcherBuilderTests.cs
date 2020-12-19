using System.IO;
using FileWatcher.Builders;
using FluentAssertions;
using Xunit;

namespace FileWatcher.Tests.Builders
{
    public class FileSystemWatcherBuilderTests
    {
        [Fact]
        public void Build_Should_Return_FileSystemWatcher_Instance()
        {
            // Arrange
            var sut = new FileSystemWatcherBuilder();
            
            // Act
            var watcher = sut.Build();
            
            // Assert
            watcher.Should().NotBeNull();
            watcher.Should().BeOfType<FileSystemWatcher>();
        }
        
        [Fact]
        public void WithPath_Should_Set_Watcher_Path_To_Input_DirectoryInfo_FullName()
        {
            // Arrange
            var sut = new FileSystemWatcherBuilder();
            var directoryInfoInput = new DirectoryInfo("c:\\");
            
            // Act
            var watcher = sut.WithPath(directoryInfoInput)
                .Build();
            
            // Assert
            watcher.Path.Should().Be(directoryInfoInput.FullName);
        }
        
        [Fact]
        public void WithFilter_Should_Set_Watcher_Filter_To_Input_Filter()
        {
            // Arrange
            const string filter = ".exe";
            var sut = new FileSystemWatcherBuilder();

            // Act
            var watcher = sut.WithFilter(filter)
                .Build();
            
            // Assert
            watcher.Filter.Should().Be(filter);
        }
        
        [Fact]
        public void WithNotifyFilter_Should_Set_Watcher_NotifyFilter_To_Input_Filter()
        {
            // Arrange
            var sut = new FileSystemWatcherBuilder();
            const NotifyFilters expectedNotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite;

            // Act
            var watcher = sut.WithNotifyFilter(expectedNotifyFilter)
                .Build();
            
            // Assert
            watcher.NotifyFilter.Should().Be(expectedNotifyFilter);
        }
        
        [Fact]
        public void WithFilters_Should_Add_Input_Filters_To_Watcher_Filters()
        {
            // Arrange
            var filters = new[] { ".exe", ".doc" };
            var sut = new FileSystemWatcherBuilder();

            // Act
            var watcher = sut.WithFilters(filters)
                .Build();
            
            // Assert
            watcher.Filters.Should().Contain(filters);
        }
    }
}