using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Common;

namespace VideoRentalSystem.Tests.Common
{
    [TestFixture]
    public class CommandProcessor_ProcessCommandSould
    {
        [TestCase(null)]
        [TestCase("")]
        public void ThrowException_WhenNullOrEmptyCommandIsPassed(string commandLine)
        {
            //Arrange
            var factoryMock = new Mock<ICommandsFactory>();
            var sut = new CommandProcessor(factoryMock.Object);

            //Act & Assert
            Assert.Throws<Exception>(() => sut.ProcessCommand(commandLine));
        }

        [TestCase("CreateCountry;Bulgaria;BG", "CreateCountry")]
        [TestCase("CountryDetails;1", "CountryDetails")]
        [TestCase("ListAllCountries", "ListAllCountries")]
        public void CallFactoryMethodCreateCommandFromString_WithSplitedCommandName(string commandLine, string expectedCommandName)
        {
            //Arrange
            var factoryMock = new Mock<ICommandsFactory>();
            var commandMock = new Mock<ICommand>();

            factoryMock.Setup(f => f.CreateCommandFromString(It.IsAny<string>())).Returns(commandMock.Object);

            var sut = new CommandProcessor(factoryMock.Object);

            //Act
            sut.ProcessCommand(commandLine);

            //Assert
            factoryMock.Verify(f => f.CreateCommandFromString(expectedCommandName), Times.Once);
        }

        [TestCase("CreateCountry;Bulgaria;BG")]
        [TestCase("CountryDetails;1")]
        [TestCase("ListAllCountries")]
        public void CallCommandExecutMethod_WithSplitedCommandParameters(string commandLine)
        {
            //Arrange
            var expectedCommandParameters = commandLine
              .Split(';')
              .Skip(1)
              .ToList();

            var factoryMock = new Mock<ICommandsFactory>();
            var commandMock = new Mock<ICommand>();

            factoryMock.Setup(f => f.CreateCommandFromString(It.IsAny<string>())).Returns(commandMock.Object);

            var sut = new CommandProcessor(factoryMock.Object);

            //Act
            sut.ProcessCommand(commandLine);

            //Assert
            commandMock.Verify(c => c.Execute(It.Is<IList<string>>(l => (l.Count == expectedCommandParameters.Count) && !l.Except(expectedCommandParameters).Any())), Times.Once);
        }

        [Test]
        public void ReturnResultOfTheCommandExecution_WhenNoErrorOccurs()
        {
            //Arrange
            var commandLine = "SomeValidCommandLine";
            var expectedExecutionResult = "expected_execution_result";
            var factoryMock = new Mock<ICommandsFactory>();
            var commandMock = new Mock<ICommand>();

            factoryMock.Setup(f => f.CreateCommandFromString(It.IsAny<string>())).Returns(commandMock.Object);
            commandMock.Setup(c => c.Execute(It.IsAny<IList<string>>())).Returns(expectedExecutionResult);
            var sut = new CommandProcessor(factoryMock.Object);

            //Act
            var result = sut.ProcessCommand(commandLine);

            //Assert
            StringAssert.AreEqualIgnoringCase(expectedExecutionResult, result);
        }
    }
}
