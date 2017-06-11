using Moq;
using NUnit.Framework;
using System;
using VideoRentalSystem.Commands.RemoveCommands;
using VideoRentalSystem.Data.Postgre.Contracts;

namespace VideoRentalSystem.Tests.Commands.RemoveCommands
{
    [TestFixture]
    public class RemoveTarifCommand_ConstructorShould
    {
        [Test]
        public void ThrowException_WhenInvalidParameterIsPassed()
        {
            //Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new RemoveTarifCommand(null));
        }

        [Test]
        public void NotThrowException_WhenInvalidParameterIsPassed()
        {
            //Arrange 
            var dbMock = new Mock<IDatabasePostgre>();

            //Act & Assert
            Assert.DoesNotThrow(() => new RemoveTarifCommand(dbMock.Object));
        }
    }
}
