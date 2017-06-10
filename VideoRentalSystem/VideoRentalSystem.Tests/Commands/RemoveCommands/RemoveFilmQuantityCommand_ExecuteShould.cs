using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VideoRentalSystem.Commands.RemoveCommands;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Tests.Commands.RemoveCommands
{
    [TestFixture]
    public class RemoveFilmQuantityCommand_ExecuteShould
    {
        [TestCase("1", "10", "invalid1", "invalid2")]
        [TestCase("1", "10", "invalid1")]
        [TestCase("1")]
        [TestCase()]
        public void ReturnNotValidParameters_WhenParametersAreDifferentOfTwo(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var sut = new RemoveFilmQuantityCommand(dbMock.Object);
            var expectedString = "Not valid number";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase("", "")]
        [TestCase("1", "")]
        [TestCase("", "1")]
        public void ReturnParametersAreEmpty_WhenSomeOfTheParametersIsEmpty(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var sut = new RemoveFilmQuantityCommand(dbMock.Object);
            var expectedString = "parameters are empty";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase("invalidNumber", "1")]
        [TestCase("3.7", "1")]
        [TestCase("invalid3", "1")]
        public void ReturnNotValidStorageId_WhenFirstParameterCannotBeParsedToInt(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var sut = new RemoveFilmQuantityCommand(dbMock.Object);
            var expectedString = "Not Valid Storage Id";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void ReturnSuchIdDoesNotExist_WhenStorageIsNotInTheRepository()
        {
            //Arrange
            var validParameters = new List<string>() { "1", "10" };
            var dbMock = new Mock<IDatabase>();
            var storageRepositoryMock = new Mock<IStorageRepository>();
            storageRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Storage, bool>>>())).Returns((Storage)null);

            dbMock.Setup(d => d.Storages).Returns(storageRepositoryMock.Object);

            var sut = new RemoveFilmQuantityCommand(dbMock.Object);
            var expectedString = "such id doesn't exist";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase("1", "invalidNumber")]
        [TestCase("1", "3.7")]
        [TestCase("1", "invalid3")]
        public void ReturnNotValidQuantity_WhenSecondParameterCannotBeParsedToInt(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var storageMock = new Mock<Storage>();
            var storageRepositoryMock = new Mock<IStorageRepository>();
            storageRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Storage, bool>>>())).Returns(storageMock.Object);

            dbMock.Setup(d => d.Storages).Returns(storageRepositoryMock.Object);

            var sut = new RemoveFilmQuantityCommand(dbMock.Object);
            var expectedString = "Not Valid Quantity";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void ReturnNotValidQuantity_WhenSecondParameterIsNegativeInt()
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var storageMock = new Mock<Storage>();
            var storageRepositoryMock = new Mock<IStorageRepository>();
            storageRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Storage, bool>>>())).Returns(storageMock.Object);

            dbMock.Setup(d => d.Storages).Returns(storageRepositoryMock.Object);

            var sut = new RemoveFilmQuantityCommand(dbMock.Object);
            var expectedString = "Not Valid Quantity";

            var invalidParameters = new List<string>() { "1", "-10" };

            //Act
            var result = sut.Execute(invalidParameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void ReturnNotValidQuantity_WhenSecondParameterIsIntButNotEnoughQuantity()
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var storageMock = new Mock<Storage>();
            var quantity = 5;
            storageMock.Object.Quantity = quantity - 1;
            var storageRepositoryMock = new Mock<IStorageRepository>();
            storageRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Storage, bool>>>())).Returns(storageMock.Object);

            dbMock.Setup(d => d.Storages).Returns(storageRepositoryMock.Object);

            var sut = new RemoveFilmQuantityCommand(dbMock.Object);
            var expectedString = "Not Valid Quantity";

            var invalidParameters = new List<string>() { "1", quantity.ToString() };

            //Act
            var result = sut.Execute(invalidParameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase(10, 0, "1", "10")]
        [TestCase(8, 2, "1", "6")]
        public void UpdateTheQuantityWithPassedValueAndReturnQuantityUpdated_WhenSecondParameterIsValidInt(int initial, int expected, params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var storageMock = new Storage();

            storageMock.Quantity = initial;

            var storageRepositoryMock = new Mock<IStorageRepository>();
            storageRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Storage, bool>>>())).Returns(storageMock);

            dbMock.Setup(d => d.Storages).Returns(storageRepositoryMock.Object);

            var sut = new RemoveFilmQuantityCommand(dbMock.Object);
            var expectedString = "Quantity updated";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            Assert.AreEqual(expected, storageMock.Quantity);
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void CallDbComplete_WhenValidParametersArePassedAndNoErrorOccurs()
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var storageMock = new Mock<Storage>();
            var quantity = 5;
            storageMock.Object.Quantity = quantity;
            var storageRepositoryMock = new Mock<IStorageRepository>();
            storageRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Storage, bool>>>())).Returns(storageMock.Object);

            dbMock.Setup(d => d.Storages).Returns(storageRepositoryMock.Object);

            var sut = new RemoveFilmQuantityCommand(dbMock.Object);

            var validParameters = new List<string>() { "1", quantity.ToString() };

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            dbMock.Verify(d => d.Complete(), Times.Once);
        }
    }
}
