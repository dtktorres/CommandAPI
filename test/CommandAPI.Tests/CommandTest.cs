using System;
using Xunit;
using CommandAPI.Models;
namespace CommandAPI.Tests
{
    public class CommandTests:IDisposable
    {
        Command testCommand;

        public CommandTests()
        {
            testCommand = new Command{
                HowTo = "Do something awesome",
                Platform = "xUnit",
                CommandLine = "dotnet test"
            };
        }

        [Fact]
        public void CanChangeHowTo()
        {
            //Act
            testCommand.HowTo = "Execute Unit Test";

            //Assert
            Assert.Equal("Execute Unit Test",testCommand.HowTo);

        }
        [Fact]
        public void CanChangePlatform()
        {
            //Act
            testCommand.Platform = "Web";

            //Assert
            Assert.Equal("Web",testCommand.Platform);

        }
        [Fact]
        public void CanChangeCommandLine()
        {
            //Act
            testCommand.CommandLine = "c++";

            //Assert
            Assert.Equal("c++",testCommand.CommandLine);
        }

        public void Dispose()
        {
           testCommand = null;
        }
    }
}