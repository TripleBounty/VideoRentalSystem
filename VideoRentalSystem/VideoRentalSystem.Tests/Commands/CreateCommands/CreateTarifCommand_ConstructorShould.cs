using Moq;
using NUnit.Framework;
using System;
using VideoRentalSystem.Commands.CreateCommands;
using VideoRentalSystem.Data.Postgre.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Tests.Commands.CreateCommands
{
    [TestFixture]
    public class CreateTarifCommand_ConstructorShould
    {
        [Test]
        public void ThrowArgumentNullException_WhenInvalidDbParameterIsPassed()
        {
            //Arrange
            var factoryMock = new Mock<IModelsFactory>();

            //Act & Arrange
            Assert.Throws<ArgumentNullException>(() => new CreateTarifCommand(null, factoryMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenInvalidFactoryParameterIsPassed()
        {
            //Arrange
            var dbMock = new Mock<IDatabasePostgre>();

            //Act & Arrange
            Assert.Throws<ArgumentNullException>(() => new CreateTarifCommand(dbMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenInvalidFactoryParameterIsPassed()
        {
            //Arrange
            var dbMock = new Mock<IDatabasePostgre>();
            var factoryMock = new Mock<IModelsFactory>();

            //Act & Arrange
            Assert.DoesNotThrow(() => new CreateTarifCommand(dbMock.Object, factoryMock.Object));
        }
    }
}
