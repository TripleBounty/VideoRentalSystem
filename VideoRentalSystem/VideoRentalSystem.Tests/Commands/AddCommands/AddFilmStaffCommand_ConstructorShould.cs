﻿using Moq;
using NUnit.Framework;
using System;
using VideoRentalSystem.Commands.AddCommands;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Tests.Commands.AddCommands
{
    [TestFixture]
    public class AddFilmStaffCommand_ConstructorShould
    {
        [Test]
        public void ThrowException_WhenInvalidParameterIsPassed()
        {
            //Arrange, Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AddFilmStaffCommand(null));
        }

        [Test]
        public void NotThrowException_WhenInvalidParameterIsPassed()
        {
            //Arrange 
            var dbMock = new Mock<IDatabase>();

            //Act & Assert
            Assert.DoesNotThrow(() => new AddFilmStaffCommand(dbMock.Object));
        }
    }
}
