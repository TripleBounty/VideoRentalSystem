using Moq;
using NUnit.Framework;
using System;
using VideoRentalSystem.Commands.RemoveCommands;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Tests.Commands.RemoveCommands
{
    [TestFixture]
    public class RemoveStoreEmployeeCommand_ConstructorShould
    {
        [Test]
        public void ThrowException_WhenInvalidParameterIsPassed()
        {
            //Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RemoveStoreEmployeeCommand(null));
        }

        [Test]
        public void NotThrowException_WhenInvalidParameterIsPassed()
        {
            //Arrange 
            var dbMock = new Mock<IDatabase>();

            //Act & Assert
            Assert.DoesNotThrow(() => new RemoveStoreEmployeeCommand(dbMock.Object));
        }
    }
}
