using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VideoRentalSystem.Commands.RemoveCommands;
using VideoRentalSystem.Data.Postgre.Contracts;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Tests.Commands.RemoveCommands
{
    [TestFixture]
    public class RemoveTarifCommand_ExecuteShould
    {

        [TestCase("1", "invalid1")]
        [TestCase()]
        public void ReturnNotValidParameters_WhenParametersAreDifferentOfOne(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabasePostgre>();
            var sut = new RemoveTarifCommand(dbMock.Object);
            var expectedString = "Not valid number";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }


        public void ReturnParametersAreEmpty_WhenSomeOfTheParametersIsEmpty()
        {
            //Arrange
            var dbMock = new Mock<IDatabasePostgre>();
            var sut = new RemoveTarifCommand(dbMock.Object);
            var expectedString = "parameters are empty";

            //Act
            var result = sut.Execute(new List<string>() { "" });

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase("invalidNumber")]
        [TestCase("3.7")]
        [TestCase("invalid3")]
        public void ReturnNotValidTarifId_WhenFirstParameterCannotBeParsedToInt(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabasePostgre>();
            var sut = new RemoveTarifCommand(dbMock.Object);
            var expectedString = "Not Valid Tarif Id";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void ReturnSuchIdDoesNotExist_WhenTarifIsNotInTheRepository()
        {
            //Arrange
            var validParameters = new List<string>() { "1" };
            var dbMock = new Mock<IDatabasePostgre>();
            var tarifRepositoryMock = new Mock<ITarifRepository>();
            tarifRepositoryMock.Setup(t => t.SingleOrDefault(It.IsAny<Expression<Func<Tarif, bool>>>())).Returns((Tarif)null);

            dbMock.Setup(d => d.Tarifs).Returns(tarifRepositoryMock.Object);

            var sut = new RemoveTarifCommand(dbMock.Object);
            var expectedString = "such id doesn't exist";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void UdateTarifPropertyIsDeleted_WhenTarifIsInTheRepository()
        {
            //Arrange
            var validParameters = new List<string>() { "1" };
            var tarifMock = new Mock<Tarif>();
            var dbMock = new Mock<IDatabasePostgre>();
            var tarifRepositoryMock = new Mock<ITarifRepository>();
            tarifRepositoryMock.Setup(t => t.SingleOrDefault(It.IsAny<Expression<Func<Tarif, bool>>>())).Returns(tarifMock.Object);

            dbMock.Setup(d => d.Tarifs).Returns(tarifRepositoryMock.Object);

            var sut = new RemoveTarifCommand(dbMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            Assert.AreEqual(true, tarifMock.Object.IsDeleted);
        }

        public void ReturnTarifUpdated_WhenTarifIsInTheRepository()
        {
            //Arrange
            var validParameters = new List<string>() { "1" };
            var tarifMock = new Mock<Tarif>();
            var dbMock = new Mock<IDatabasePostgre>();
            var tarifRepositoryMock = new Mock<ITarifRepository>();
            tarifRepositoryMock.Setup(t => t.SingleOrDefault(It.IsAny<Expression<Func<Tarif, bool>>>())).Returns(tarifMock.Object);

            dbMock.Setup(d => d.Tarifs).Returns(tarifRepositoryMock.Object);

            var sut = new RemoveTarifCommand(dbMock.Object);
            var expectedResult = "Tarif updated";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedResult, result);
        }

        [Test]
        public void CallDbComplete_WhenValidParametersArePassedAndNoErrorOccurs()
        {
            //Arrange
            var validParameters = new List<string>() { "1" };
            var tarifMock = new Mock<Tarif>();
            var dbMock = new Mock<IDatabasePostgre>();
            var tarifRepositoryMock = new Mock<ITarifRepository>();
            tarifRepositoryMock.Setup(t => t.SingleOrDefault(It.IsAny<Expression<Func<Tarif, bool>>>())).Returns(tarifMock.Object);

            dbMock.Setup(d => d.Tarifs).Returns(tarifRepositoryMock.Object);

            var sut = new RemoveTarifCommand(dbMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            dbMock.Verify(d => d.Complete(), Times.Once);
        }
    }
}
