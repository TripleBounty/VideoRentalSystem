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
    public class CreateCountryCommand_ExecuteShould
    {
        [TestCase("Bulgaria", "BG", "invalid2")]
        [TestCase("Bulgaria")]
        [TestCase()]
        public void ReturnNotValidParameters_WhenParametersAreDifferentOfThree(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var sut = new CreateCountryCommand(dbMock.Object, factoryMock.Object);
            var expectedString = "Not valid number";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase("", "BG")]
        [TestCase("Bulgaria", "")]
        [TestCase("", "")]
        public void ReturnParametersAreEmpty_WhenSomeOfTheParametersIsEmpty(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var sut = new CreateCountryCommand(dbMock.Object, factoryMock.Object);
            var expectedString = "parameters are empty";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void CallCreateountryMethodOfTheFactory_WithCountryNameParameter()
        {
            //Arrange
            var countryName = "Bulgaria";
            var countryCode = "BG";
            var validParameters = new List<string>() { countryName, countryCode };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var countryMock = new Mock<Country>();
            factoryMock.Setup(f => f.CreateCountry(It.IsAny<string>(), It.IsAny<string>())).Returns(countryMock.Object);

            var countryRepositoryMock = new Mock<ICountryRepository>();
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);
            var sut = new CreateCountryCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            factoryMock.Verify(f => f.CreateCountry(countryName, It.IsAny<string>()), Times.Once);
        }

        [Test]
        public void CallCreateountryMethodOfTheFactory_WithCountryCodeParameter()
        {
            //Arrange
            var countryName = "Bulgaria";
            var countryCode = "BG";
            var validParameters = new List<string>() { countryName, countryCode };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var countryMock = new Mock<Country>();
            factoryMock.Setup(f => f.CreateCountry(It.IsAny<string>(), It.IsAny<string>())).Returns(countryMock.Object);

            var countryRepositoryMock = new Mock<ICountryRepository>();
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);
            var sut = new CreateCountryCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            factoryMock.Verify(f => f.CreateCountry(It.IsAny<string>(), countryCode), Times.Once);
        }


        [Test]
        public void CallAddMethodOfDataBaseCountryRepository_WithCreatedCountry()
        {
            //Arrange
            var countryName = "Bulgaria";
            var countryCode = "BG";
            var validParameters = new List<string>() { countryName, countryCode };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var countryMock = new Mock<Country>();
            factoryMock.Setup(f => f.CreateCountry(It.IsAny<string>(), It.IsAny<string>())).Returns(countryMock.Object);

            var countryRepositoryMock = new Mock<ICountryRepository>();
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);
            var sut = new CreateCountryCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            countryRepositoryMock.Verify(c => c.Add(countryMock.Object), Times.Once);
        }

        [Test]
        public void CallCompleteOfDataBase_WhenCountryIsCreatedAddress()
        {
            //Arrange
            var countryName = "Bulgaria";
            var countryCode = "BG";
            var validParameters = new List<string>() { countryName, countryCode };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var countryMock = new Mock<Country>();
            factoryMock.Setup(f => f.CreateCountry(It.IsAny<string>(), It.IsAny<string>())).Returns(countryMock.Object);

            var countryRepositoryMock = new Mock<ICountryRepository>();
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);
            var sut = new CreateCountryCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            dbMock.Verify(d => d.Complete(), Times.Once);
        }

        [Test]
        public void ReturnAddressCreated_WhenAddressIsCreatedAddress()
        {
            //Arrange
            var countryName = "Bulgaria";
            var countryCode = "BG";
            var validParameters = new List<string>() { countryName, countryCode };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var countryMock = new Mock<Country>();
            factoryMock.Setup(f => f.CreateCountry(It.IsAny<string>(), It.IsAny<string>())).Returns(countryMock.Object);

            var countryRepositoryMock = new Mock<ICountryRepository>();
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);
            var sut = new CreateCountryCommand(dbMock.Object, factoryMock.Object);

            var expectedResult = "Country created";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedResult, result);
        }
    }
}
