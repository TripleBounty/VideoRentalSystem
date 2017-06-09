using Moq;
using NUnit.Framework;
using System;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Common;

namespace VideoRentalSystem.Tests.Common
{
    [TestFixture]
    public class CommandProcessor_ConstructorSould
    {
        [Test]
        public void ThrowArgumentNullException_WhenInvalidCommandFactoryIsPassed()
        {
            //Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new CommandProcessor(null));
        }

        [Test]
        public void NotThrow_WhenValidCommandFactoryIsPassed()
        {
            //Arrange
            var factoryMock = new Mock<ICommandsFactory>();

            //Act & Assert
            Assert.DoesNotThrow(() => new CommandProcessor(factoryMock.Object));
        }
    }
}
