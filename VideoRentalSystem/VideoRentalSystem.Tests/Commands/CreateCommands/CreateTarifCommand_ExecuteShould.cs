using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VideoRentalSystem.Commands.CreateCommands;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Data.Postgre.Contracts;
using VideoRentalSystem.Models;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Tests.Commands.CreateCommands
{
    [TestFixture]
    public class CreateTarifCommand_ExecuteShould
    {
        [TestCase("name", "10", "1.5", "invalid2")]
        [TestCase("name", "10")]
        [TestCase("name")]
        [TestCase()]
        public void ReturnNotValidParameters_WhenParametersAreDifferentOfThree(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabasePostgre>();
            var factoryMock = new Mock<IModelsFactory>();

            var sut = new CreateTarifCommand(dbMock.Object, factoryMock.Object);
            var expectedString = "Not valid number";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase("", "10", "1.5")]
        [TestCase("name", "", "1.5")]
        [TestCase("name", "10", "")]
        [TestCase("", "", "")]
        public void ReturnParametersAreEmpty_WhenSomeOfTheParametersIsEmpty(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabasePostgre>();
            var factoryMock = new Mock<IModelsFactory>();

            var sut = new CreateTarifCommand(dbMock.Object, factoryMock.Object);
            var expectedString = "parameters are empty";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase("name", "10.5", "1.5")]
        [TestCase("name", "invalid", "1.5")]
        [TestCase("name", "invalid10", "1.5")]
        public void ReturnNotValidDay_WhenDayParameterCannotBeParsedToInt(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabasePostgre>();
            var factoryMock = new Mock<IModelsFactory>();

            var sut = new CreateTarifCommand(dbMock.Object, factoryMock.Object);
            var expectedString = "Not Valid Day";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase("name", "1", "invalid")]
        [TestCase("name", "1", "invalid10")]
        public void ReturnNotValidPrice_WhenPriceParameterCannotBeParsedToInt(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabasePostgre>();
            var factoryMock = new Mock<IModelsFactory>();

            var sut = new CreateTarifCommand(dbMock.Object, factoryMock.Object);
            var expectedString = "Not Valid Price";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void ReturnAlreadyExist_WhenTarifWithSameDaysExist()
        {
            //Arrange
            var nameParam = "Default";
            var dayParam = 10;
            var price = 1.5M;
            var validParameters = new List<string>() { nameParam, dayParam.ToString(), price.ToString() };
            var dbMock = new Mock<IDatabasePostgre>();
            var factoryMock = new Mock<IModelsFactory>();

            var tarifMock = new Mock<Tarif>();

            var tarifRepositoryMock = new Mock<ITarifRepository>();
            tarifRepositoryMock.Setup(t => t.SingleOrDefault(It.IsAny<Expression<Func<Tarif, bool>>>())).Returns(tarifMock.Object);
            dbMock.Setup(d => d.Tarifs).Returns(tarifRepositoryMock.Object);

            var sut = new CreateTarifCommand(dbMock.Object, factoryMock.Object);
            var expectedResult = "already exist";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedResult, result);
        }

        [Test]
        public void CallCreateTarifMethodOfTheFactory_WithNameParameter()
        {
            //Arrange
            var nameParam = "Default";
            var dayParam = 10;
            var price = 1.5M;
            var validParameters = new List<string>() { nameParam, dayParam.ToString(), price.ToString() };
            var dbMock = new Mock<IDatabasePostgre>();
            var factoryMock = new Mock<IModelsFactory>();

            var tarifMock = new Mock<Tarif>();
            factoryMock.Setup(f => f.CreateTarif(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<decimal>())).Returns(tarifMock.Object);

            var tarifRepositoryMock = new Mock<ITarifRepository>();
            dbMock.Setup(d => d.Tarifs).Returns(tarifRepositoryMock.Object);

            var sut = new CreateTarifCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            factoryMock.Verify(f => f.CreateTarif(nameParam, It.IsAny<int>(), It.IsAny<decimal>()), Times.Once);
        }

        [Test]
        public void CallCreateTarifMethodOfTheFactory_WithDayParameter()
        {
            //Arrange
            var nameParam = "Default";
            var dayParam = 10;
            var price = 1.5M;
            var validParameters = new List<string>() { nameParam, dayParam.ToString(), price.ToString() };
            var dbMock = new Mock<IDatabasePostgre>();
            var factoryMock = new Mock<IModelsFactory>();

            var tarifMock = new Mock<Tarif>();
            factoryMock.Setup(f => f.CreateTarif(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<decimal>())).Returns(tarifMock.Object);

            var tarifRepositoryMock = new Mock<ITarifRepository>();
            dbMock.Setup(d => d.Tarifs).Returns(tarifRepositoryMock.Object);

            var sut = new CreateTarifCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            factoryMock.Verify(f => f.CreateTarif(It.IsAny<string>(), dayParam, It.IsAny<decimal>()), Times.Once);
        }

        [Test]
        public void CallCreateTarifMethodOfTheFactory_WithPriceParameter()
        {
            //Arrange
            var nameParam = "Default";
            var dayParam = 10;
            var price = 1.5M;
            var validParameters = new List<string>() { nameParam, dayParam.ToString(), price.ToString() };
            var dbMock = new Mock<IDatabasePostgre>();
            var factoryMock = new Mock<IModelsFactory>();

            var tarifMock = new Mock<Tarif>();
            factoryMock.Setup(f => f.CreateTarif(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<decimal>())).Returns(tarifMock.Object);

            var tarifRepositoryMock = new Mock<ITarifRepository>();
            dbMock.Setup(d => d.Tarifs).Returns(tarifRepositoryMock.Object);

            var sut = new CreateTarifCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            factoryMock.Verify(f => f.CreateTarif(It.IsAny<string>(), It.IsAny<int>(), price), Times.Once);
        }


        [Test]
        public void CallAddMethodOfDataBaseTarifRepository_WithCreatedTarif()
        {
            //Arrange
            var nameParam = "Default";
            var dayParam = 10;
            var price = 1.5M;
            var validParameters = new List<string>() { nameParam, dayParam.ToString(), price.ToString() };
            var dbMock = new Mock<IDatabasePostgre>();
            var factoryMock = new Mock<IModelsFactory>();

            var tarifMock = new Mock<Tarif>();
            factoryMock.Setup(f => f.CreateTarif(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<decimal>())).Returns(tarifMock.Object);

            var tarifRepositoryMock = new Mock<ITarifRepository>();
            dbMock.Setup(d => d.Tarifs).Returns(tarifRepositoryMock.Object);

            var sut = new CreateTarifCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            tarifRepositoryMock.Verify(t => t.Add(tarifMock.Object), Times.Once);
        }

        [Test]
        public void CallCompleteOfDataBase_WhenTarifIsCreatedAddress()
        {
            //Arrange
            var nameParam = "Default";
            var dayParam = 10;
            var price = 1.5M;
            var validParameters = new List<string>() { nameParam, dayParam.ToString(), price.ToString() };
            var dbMock = new Mock<IDatabasePostgre>();
            var factoryMock = new Mock<IModelsFactory>();

            var tarifMock = new Mock<Tarif>();
            factoryMock.Setup(f => f.CreateTarif(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<decimal>())).Returns(tarifMock.Object);

            var tarifRepositoryMock = new Mock<ITarifRepository>();
            dbMock.Setup(d => d.Tarifs).Returns(tarifRepositoryMock.Object);

            var sut = new CreateTarifCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            dbMock.Verify(d => d.Complete(), Times.Once);
        }

        [Test]
        public void ReturnTarifCreated_WhenTarifIsCreatedAddress()
        {
            //Arrange
            var nameParam = "Default";
            var dayParam = 10;
            var price = 1.5M;
            var validParameters = new List<string>() { nameParam, dayParam.ToString(), price.ToString() };
            var dbMock = new Mock<IDatabasePostgre>();
            var factoryMock = new Mock<IModelsFactory>();

            var tarifMock = new Mock<Tarif>();
            factoryMock.Setup(f => f.CreateTarif(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<decimal>())).Returns(tarifMock.Object);

            var tarifRepositoryMock = new Mock<ITarifRepository>();
            dbMock.Setup(d => d.Tarifs).Returns(tarifRepositoryMock.Object);

            var sut = new CreateTarifCommand(dbMock.Object, factoryMock.Object);

            var expectedResult = "Tarif created";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedResult, result);
        }
    }
}
