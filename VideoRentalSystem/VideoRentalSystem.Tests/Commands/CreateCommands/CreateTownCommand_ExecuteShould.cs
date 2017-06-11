using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VideoRentalSystem.Commands.CreateCommands;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Tests.Commands.CreateCommands
{
    [TestFixture]
    public class CreateTownCommand_ExecuteShould
    {
        [TestCase("Sofia", "1", "invalid2")]
        [TestCase("Sofia")]
        [TestCase()]
        public void ReturnNotValidParameters_WhenParametersAreDifferentOfThree(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var sut = new CreateTownCommand(dbMock.Object, factoryMock.Object);
            var expectedString = "Not valid number";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase("", "1")]
        [TestCase("Sofia", "")]
        [TestCase("", "")]
        public void ReturnParametersAreEmpty_WhenSomeOfTheParametersIsEmpty(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var sut = new CreateTownCommand(dbMock.Object, factoryMock.Object);
            var expectedString = "parameters are empty";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase("street", "invalid")]
        [TestCase("street", "1.5")]
        [TestCase("street", "invalid1")]
        public void ReturnNotValidCountryId_WhenCountryParameterCannotBeParsedToInt(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var sut = new CreateTownCommand(dbMock.Object, factoryMock.Object);
            var expectedString = "Not Valid Country Id";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void ReturnSuchIdDoesNotExist_WhenTownIsNotInTheRepository()
        {
            //Arrange
            var validParameters = new List<string>() { "Sofia", "1" };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var countryRepositoryMock = new Mock<ICountryRepository>();
            countryRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Country, bool>>>())).Returns((Country)null);
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);

            var sut = new CreateTownCommand(dbMock.Object, factoryMock.Object);
            var expectedString = "such id doesn't exist";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void CallCreateAddressMethodOfTheFactory_WithNameParameter()
        {
            //Arrange
            var townName = "Sofia";
            var countryId = 1;
            var validParameters = new List<string>() { townName, countryId.ToString() };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var townMock = new Mock<Town>();
            factoryMock.Setup(f => f.CreateTown(It.IsAny<string>(), It.IsAny<Country>())).Returns(townMock.Object);

            var countryMock = new Mock<Country>();

            var countryRepositoryMock = new Mock<ICountryRepository>();
            countryRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Country, bool>>>())).Returns(countryMock.Object);
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);

            var townRepositoryMock = new Mock<ITownRepository>();
            dbMock.Setup(d => d.Towns).Returns(townRepositoryMock.Object);
            var sut = new CreateTownCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            factoryMock.Verify(f => f.CreateTown(townName, It.IsAny<Country>()), Times.Once);
        }

        [Test]
        public void CallCreateAddressMethodOfTheFactory_WithCountryParameter()
        {
            //Arrange
            var townName = "Sofia";
            var countryId = 1;
            var validParameters = new List<string>() { townName, countryId.ToString() };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var townMock = new Mock<Town>();
            factoryMock.Setup(f => f.CreateTown(It.IsAny<string>(), It.IsAny<Country>())).Returns(townMock.Object);

            var countryMock = new Mock<Country>();

            var countryRepositoryMock = new Mock<ICountryRepository>();
            countryRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Country, bool>>>())).Returns(countryMock.Object);
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);

            var townRepositoryMock = new Mock<ITownRepository>();
            dbMock.Setup(d => d.Towns).Returns(townRepositoryMock.Object);
            var sut = new CreateTownCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            factoryMock.Verify(f => f.CreateTown(It.IsAny<string>(), countryMock.Object), Times.Once);
        }

        [Test]
        public void CallAddMethodOfDataBaseTownRepository_WithCreatedTown()
        {
            //Arrange
            var townName = "Sofia";
            var countryId = 1;
            var validParameters = new List<string>() { townName, countryId.ToString() };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var townMock = new Mock<Town>();
            factoryMock.Setup(f => f.CreateTown(It.IsAny<string>(), It.IsAny<Country>())).Returns(townMock.Object);

            var countryMock = new Mock<Country>();

            var countryRepositoryMock = new Mock<ICountryRepository>();
            countryRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Country, bool>>>())).Returns(countryMock.Object);
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);

            var townRepositoryMock = new Mock<ITownRepository>();
            dbMock.Setup(d => d.Towns).Returns(townRepositoryMock.Object);
            var sut = new CreateTownCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            townRepositoryMock.Verify(t => t.Add(townMock.Object), Times.Once);
        }

        [Test]
        public void CallCompleteOfDataBase_WhenAddressIsCreatedAddress()
        {
            //Arrange
            var townName = "Sofia";
            var countryId = 1;
            var validParameters = new List<string>() { townName, countryId.ToString() };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var townMock = new Mock<Town>();
            factoryMock.Setup(f => f.CreateTown(It.IsAny<string>(), It.IsAny<Country>())).Returns(townMock.Object);

            var countryMock = new Mock<Country>();

            var countryRepositoryMock = new Mock<ICountryRepository>();
            countryRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Country, bool>>>())).Returns(countryMock.Object);
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);

            var townRepositoryMock = new Mock<ITownRepository>();
            dbMock.Setup(d => d.Towns).Returns(townRepositoryMock.Object);
            var sut = new CreateTownCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            dbMock.Verify(d => d.Complete(), Times.Once);
        }

        [Test]
        public void ReturnAddressCreated_WhenAddressIsCreatedAddress()
        {
            //Arrange
            var townName = "Sofia";
            var countryId = 1;
            var validParameters = new List<string>() { townName, countryId.ToString() };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var townMock = new Mock<Town>();
            factoryMock.Setup(f => f.CreateTown(It.IsAny<string>(), It.IsAny<Country>())).Returns(townMock.Object);

            var countryMock = new Mock<Country>();

            var countryRepositoryMock = new Mock<ICountryRepository>();
            countryRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Country, bool>>>())).Returns(countryMock.Object);
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);

            var townRepositoryMock = new Mock<ITownRepository>();
            dbMock.Setup(d => d.Towns).Returns(townRepositoryMock.Object);
            var sut = new CreateTownCommand(dbMock.Object, factoryMock.Object);

            var expectedResult = "Town created";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedResult, result);
        }
    }
}
