using System;
using System.Collections.Generic;
using System.CommandLine;
using System.IO;
using AutoFixture;
using FileWatcher.CommandInitializers;
using FileWatcher.Tests.TestInfrastructure.AutoFixture;
using FluentAssertions;
using Xunit;

namespace FileWatcher.Tests.CommandInitializers
{
    public class CopyCommandInitializerTests
    {
        private readonly IFixture testFixture;

        public CopyCommandInitializerTests()
        {
            testFixture = TestFixture.CreateAutoMockingContainer();
            testFixture.Register(() => new DirectoryInfo("c:\\"));
        }

        [Fact]
        public void Initialize_Should_Create_Command_Instance_With_Correct_Name()
        {
            // Arrange
            const string expectedCommandName = "copyonchange";
            var sut = testFixture.Create<CopyCommandInitializer>();
            
            // Act
            var command = sut.Initialize();
            
            // Assert
            command.Should().BeOfType<Command>();
            command.Name.Should().Be(expectedCommandName);
        }
        
        [Fact]
        public void Initialize_Should_Create_Command_Instance_With_Correct_Arguments_And_Options()
        {
            // Arrange
            var sut = testFixture.Create<CopyCommandInitializer>();
            var expectedArguments = new List<Argument>
            {
                new Argument<DirectoryInfo>("source", "The directory that files should be copied from"),
                new Argument<DirectoryInfo>("target", "The directory that files should be copied to"),
            };
            var expectedOptions = new List<Option>
            {
                new Option<string[]>(
                    "--filter",
                    description: "Comma-separated list of file extensions to filter by.",
                    getDefaultValue: Array.Empty<string>),
            };
            
            // Act
            var command = sut.Initialize();
            
            // Assert
            command.Arguments.Should().BeEquivalentTo(expectedArguments, x =>
            {
                return x.Including(y => y.Name)
                    .Including(y => y.Description)
                    .Including(y => y.ArgumentType)
                    .WithStrictOrdering();
            });
            
            command.Options.Should().BeEquivalentTo(expectedOptions, x =>
            {
                return x.Including(y => y.Name)
                    .Including(y => y.Description)
                    .Including(y => y.Argument.ArgumentType)
                    .WithStrictOrdering();
            });
        }
    }
}