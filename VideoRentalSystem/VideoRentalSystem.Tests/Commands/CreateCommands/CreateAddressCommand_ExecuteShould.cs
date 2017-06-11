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
    public class CreateAddressCommand_ExecuteShould
    {
        [TestCase("street", "postalcode", "1", "invalid2")]
        [TestCase("street", "postalcode")]
        [TestCase("street")]
        [TestCase()]
        public void ReturnNotValidParameters_WhenParametersAreDifferentOfThree(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var sut = new CreateAddressCommand(dbMock.Object, factoryMock.Object);
            var expectedString = "Not valid number";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase("", "postalcode", "1")]
        [TestCase("street", "", "1")]
        [TestCase("street", "postalcode", "")]
        [TestCase("", "", "")]
        public void ReturnParametersAreEmpty_WhenSomeOfTheParametersIsEmpty(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var sut = new CreateAddressCommand(dbMock.Object, factoryMock.Object);
            var expectedString = "parameters are empty";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase("street", "postalcode", "invalid")]
        [TestCase("street", "postalcode", "1.5")]
        [TestCase("street", "postalcode", "invalid1")]
        public void ReturnNotValidTownId_WhenThirdParameterCannotBeParsedToInt(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var sut = new CreateAddressCommand(dbMock.Object, factoryMock.Object);
            var expectedString = "Not Valid Town Id";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void ReturnSuchIdDoesNotExist_WhenTownIsNotInTheRepository()
        {
            //Arrange
            var validParameters = new List<string>() { "street", "postalcode", "1" };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var townRepositoryMock = new Mock<ITownRepository>();
            townRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Town, bool>>>())).Returns((Town)null);
            dbMock.Setup(d => d.Towns).Returns(townRepositoryMock.Object);

            var sut = new CreateAddressCommand(dbMock.Object, factoryMock.Object);
            var expectedString = "such id doesn't exist";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void CallCreateAddressMethodOfTheFactory_WithStreetParameter()
        {
            //Arrange
            var streetParam = "street";
            var postalCodeParam = "1715";
            var townId = 1;
            var validParameters = new List<string>() { streetParam, postalCodeParam, townId.ToString() };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var addressMock = new Mock<Address>();
            factoryMock.Setup(f => f.CreateAddress(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Town>())).Returns(addressMock.Object);

            var townMock = new Mock<Town>();

            var townRepositoryMock = new Mock<ITownRepository>();
            townRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Town, bool>>>())).Returns(townMock.Object);
            dbMock.Setup(d => d.Towns).Returns(townRepositoryMock.Object);

            var addressRepositpryMock = new Mock<IAddessRepository>();
            dbMock.Setup(d => d.Addesses).Returns(addressRepositpryMock.Object);
            var sut = new CreateAddressCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            factoryMock.Verify(f => f.CreateAddress(streetParam, It.IsAny<string>(), It.IsAny<Town>()), Times.Once);
        }

        [Test]
        public void CallCreateAddressMethodOfTheFactory_WithPostalCodeParameter()
        {
            //Arrange
            var streetParam = "street";
            var postalCodeParam = "1715";
            var townId = 1;
            var validParameters = new List<string>() { streetParam, postalCodeParam, townId.ToString() };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var addressMock = new Mock<Address>();
            factoryMock.Setup(f => f.CreateAddress(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Town>())).Returns(addressMock.Object);

            var townMock = new Mock<Town>();

            var townRepositoryMock = new Mock<ITownRepository>();
            townRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Town, bool>>>())).Returns(townMock.Object);
            dbMock.Setup(d => d.Towns).Returns(townRepositoryMock.Object);

            var addressRepositpryMock = new Mock<IAddessRepository>();
            dbMock.Setup(d => d.Addesses).Returns(addressRepositpryMock.Object);
            var sut = new CreateAddressCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            factoryMock.Verify(f => f.CreateAddress(It.IsAny<string>(), postalCodeParam, It.IsAny<Town>()), Times.Once);
        }

        [Test]
        public void CallCreateAddressMethodOfTheFactory_WithTownParameter()
        {
            //Arrange
            var streetParam = "street";
            var postalCodeParam = "1715";
            var townId = 1;
            var validParameters = new List<string>() { streetParam, postalCodeParam, townId.ToString() };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var addressMock = new Mock<Address>();
            factoryMock.Setup(f => f.CreateAddress(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Town>())).Returns(addressMock.Object);

            var townMock = new Mock<Town>();

            var townRepositoryMock = new Mock<ITownRepository>();
            townRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Town, bool>>>())).Returns(townMock.Object);
            dbMock.Setup(d => d.Towns).Returns(townRepositoryMock.Object);

            var addressRepositpryMock = new Mock<IAddessRepository>();
            dbMock.Setup(d => d.Addesses).Returns(addressRepositpryMock.Object);
            var sut = new CreateAddressCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            factoryMock.Verify(f => f.CreateAddress(It.IsAny<string>(), It.IsAny<string>(), townMock.Object), Times.Once);
        }

        [Test]
        public void CallAddMethodOfDataBaseAddressRepository_WithCreatedAddress()
        {
            //Arrange
            var streetParam = "street";
            var postalCodeParam = "1715";
            var townId = 1;
            var validParameters = new List<string>() { streetParam, postalCodeParam, townId.ToString() };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var addressMock = new Mock<Address>();
            factoryMock.Setup(f => f.CreateAddress(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Town>())).Returns(addressMock.Object);

            var townMock = new Mock<Town>();

            var townRepositoryMock = new Mock<ITownRepository>();
            townRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Town, bool>>>())).Returns(townMock.Object);
            dbMock.Setup(d => d.Towns).Returns(townRepositoryMock.Object);

            var addressRepositoryMock = new Mock<IAddessRepository>();
            dbMock.Setup(d => d.Addesses).Returns(addressRepositoryMock.Object);
            var sut = new CreateAddressCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            addressRepositoryMock.Verify(a => a.Add(addressMock.Object), Times.Once);
        }

        [Test]
        public void CallCompleteOfDataBase_WhenAddressIsCreatedAddress()
        {
            //Arrange
            var streetParam = "street";
            var postalCodeParam = "1715";
            var townId = 1;
            var validParameters = new List<string>() { streetParam, postalCodeParam, townId.ToString() };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var addressMock = new Mock<Address>();
            factoryMock.Setup(f => f.CreateAddress(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Town>())).Returns(addressMock.Object);

            var townMock = new Mock<Town>();

            var townRepositoryMock = new Mock<ITownRepository>();
            townRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Town, bool>>>())).Returns(townMock.Object);
            dbMock.Setup(d => d.Towns).Returns(townRepositoryMock.Object);

            var addressRepositoryMock = new Mock<IAddessRepository>();
            dbMock.Setup(d => d.Addesses).Returns(addressRepositoryMock.Object);
            var sut = new CreateAddressCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            dbMock.Verify(d => d.Complete(), Times.Once);
        }

        [Test]
        public void ReturnAddressCreated_WhenAddressIsCreatedAddress()
        {
            //Arrange
            var streetParam = "street";
            var postalCodeParam = "1715";
            var townId = 1;
            var validParameters = new List<string>() { streetParam, postalCodeParam, townId.ToString() };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var addressMock = new Mock<Address>();
            factoryMock.Setup(f => f.CreateAddress(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Town>())).Returns(addressMock.Object);

            var townMock = new Mock<Town>();

            var townRepositoryMock = new Mock<ITownRepository>();
            townRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Town, bool>>>())).Returns(townMock.Object);
            dbMock.Setup(d => d.Towns).Returns(townRepositoryMock.Object);

            var addressRepositoryMock = new Mock<IAddessRepository>();
            dbMock.Setup(d => d.Addesses).Returns(addressRepositoryMock.Object);
            var sut = new CreateAddressCommand(dbMock.Object, factoryMock.Object);

            var expectedResult = "Address created";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedResult, result);
        }
    }
}
