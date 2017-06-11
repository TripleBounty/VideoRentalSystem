﻿using Moq;
using NUnit.Framework;
using System;
using VideoRentalSystem.Commands.CreateCommands;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Tests.Commands.CreateCommands
{
    [TestFixture]
    public class CreateTownCommand_ConstructorShould
    {
        [Test]
        public void ThrowArgumentNullException_WhenInvalidDbParameterIsPassed()
        {
            //Arrange
            var factoryMock = new Mock<IModelsFactory>();

            //Act & Arrange
            Assert.Throws<ArgumentNullException>(() => new CreateTownCommand(null, factoryMock.Object));
        }

        [Test]
        public void ThrowArgumentNullException_WhenInvalidFactoryParameterIsPassed()
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();

            //Act & Arrange
            Assert.Throws<ArgumentNullException>(() => new CreateTownCommand(dbMock.Object, null));
        }

        [Test]
        public void NotThrow_WhenInvalidFactoryParameterIsPassed()
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            //Act & Arrange
            Assert.DoesNotThrow(() => new CreateTownCommand(dbMock.Object, factoryMock.Object));
        }
    }
}
