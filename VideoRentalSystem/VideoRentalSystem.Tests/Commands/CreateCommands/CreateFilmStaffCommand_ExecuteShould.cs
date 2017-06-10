using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VideoRentalSystem.Commands.CreateCommands;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;
using VideoRentalSystem.Models.Enum;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Tests.Commands.CreateCommands
{
    [TestFixture]
    public class CreateFilmStaffCommand_ExecuteShould
    {
        [TestCase("firstName", "lastName", "10/10/1980", "1", "Writer", "", "")]
        [TestCase("firstName", "lastName", "10/10/1980", "1", "Writer", "")]
        [TestCase("firstName", "lastName", "", "")]
        [TestCase()]
        public void ReturnNotValidParameters_WhenParametersAreDifferentOfThree(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var sut = new CreateFilmStaffCommand(dbMock.Object, factoryMock.Object);
            var expectedString = "Not valid number";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase("", "lastName", "10/10/1980", "1", "Writer")]
        [TestCase("firstName", "", "10/10/1980", "1", "Writer")]
        [TestCase("firstName", "lastName", "", "1", "Writer")]
        [TestCase("firstName", "lastName", "10/10/1980", "", "Writer")]
        [TestCase("firstName", "lastName", "10/10/1980", "1", "")]
        [TestCase("", "", "", "", "")]
        public void ReturnParametersAreEmpty_WhenSomeOfTheParametersIsEmpty(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var sut = new CreateFilmStaffCommand(dbMock.Object, factoryMock.Object);
            var expectedString = "parameters are empty";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase("firstName", "lastName", "10/10/1980", "invalid", "Writer")]
        [TestCase("firstName", "lastName", "10/10/1980", "1.5", "Writer")]
        [TestCase("firstName", "lastName", "10/10/1980", "invalid1", "Writer")]
        public void ReturnNotValidCountryId_WhenCountryParameterCannotBeParsedToInt(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var sut = new CreateFilmStaffCommand(dbMock.Object, factoryMock.Object);
            var expectedString = "Not Valid Country Id";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void ReturnSuchIdDoesNotExist_WhenCountryIsNotInTheRepository()
        {
            //Arrange
            var validParameters = new List<string>() { "firstName", "lastName", "10/10/1980", "1", "Writer" };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var countryRepositoryMock = new Mock<ICountryRepository>();
            countryRepositoryMock.Setup(c => c.SingleOrDefault(It.IsAny<Expression<Func<Country, bool>>>())).Returns((Country)null);
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);

            var sut = new CreateFilmStaffCommand(dbMock.Object, factoryMock.Object);
            var expectedString = "such id doesn't exist";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void ReturnNotValidFilmStaffType_WhenCannotParceTheType()
        {
            //Arrange
            var validParameters = new List<string>() { "firstName", "lastName", "10/10/1980", "1", "S" };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var countryMock = new Mock<Country>();

            var countryRepositoryMock = new Mock<ICountryRepository>();
            countryRepositoryMock.Setup(c => c.SingleOrDefault(It.IsAny<Expression<Func<Country, bool>>>())).Returns(countryMock.Object);
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);

            var sut = new CreateFilmStaffCommand(dbMock.Object, factoryMock.Object);
            var expectedString = "Not Valid Film Staff type";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void CallCreateFilmStaff_WithFirstNameParameter()
        {
            //Arrange
            var fistName = "firstName";
            var lastName = "lastName";
            var date = "10/10/1980";
            var countryId = "1";
            var type = StaffType.Writer;
            var validParameters = new List<string>() { fistName, lastName, date, countryId, type.ToString() };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var countryMock = new Mock<Country>();

            var countryRepositoryMock = new Mock<ICountryRepository>();
            countryRepositoryMock.Setup(c => c.SingleOrDefault(It.IsAny<Expression<Func<Country, bool>>>())).Returns(countryMock.Object);
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);

            var filmStaffRepositoryMock = new Mock<IFilmStaffRepository>();
            dbMock.Setup(d => d.FilmStaffs).Returns(filmStaffRepositoryMock.Object);

            var sut = new CreateFilmStaffCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            factoryMock.Verify(f => f.CreateFilmStaff(fistName, It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<Country>(), It.IsAny<StaffType>()), Times.Once);
        }

        [Test]
        public void CallCreateFilmStaff_WithLastNameParameter()
        {
            //Arrange
            var fistName = "firstName";
            var lastName = "lastName";
            var date = "10/10/1980";
            var countryId = "1";
            var type = StaffType.Writer;
            var validParameters = new List<string>() { fistName, lastName, date, countryId, type.ToString() };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var countryMock = new Mock<Country>();

            var countryRepositoryMock = new Mock<ICountryRepository>();
            countryRepositoryMock.Setup(c => c.SingleOrDefault(It.IsAny<Expression<Func<Country, bool>>>())).Returns(countryMock.Object);
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);

            var filmStaffRepositoryMock = new Mock<IFilmStaffRepository>();
            dbMock.Setup(d => d.FilmStaffs).Returns(filmStaffRepositoryMock.Object);

            var sut = new CreateFilmStaffCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            factoryMock.Verify(f => f.CreateFilmStaff(It.IsAny<string>(), lastName, It.IsAny<DateTime>(), It.IsAny<Country>(), It.IsAny<StaffType>()), Times.Once);
        }

        [Test]
        public void CallCreateFilmStaff_WithBirthDateParameter()
        {
            //Arrange
            var fistName = "firstName";
            var lastName = "lastName";
            var date = "10/10/1980";
            var countryId = "1";
            var type = StaffType.Writer;
            var validParameters = new List<string>() { fistName, lastName, date, countryId, type.ToString() };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var countryMock = new Mock<Country>();

            var countryRepositoryMock = new Mock<ICountryRepository>();
            countryRepositoryMock.Setup(c => c.SingleOrDefault(It.IsAny<Expression<Func<Country, bool>>>())).Returns(countryMock.Object);
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);

            var filmStaffRepositoryMock = new Mock<IFilmStaffRepository>();
            dbMock.Setup(d => d.FilmStaffs).Returns(filmStaffRepositoryMock.Object);

            var sut = new CreateFilmStaffCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            factoryMock.Verify(f => f.CreateFilmStaff(It.IsAny<string>(), It.IsAny<string>(), Convert.ToDateTime(date), It.IsAny<Country>(), It.IsAny<StaffType>()), Times.Once);
        }

        [Test]
        public void CallCreateFilmStaff_WithCountryParameter()
        {
            //Arrange
            var fistName = "firstName";
            var lastName = "lastName";
            var date = "10/10/1980";
            var countryId = "1";
            var type = StaffType.Writer;
            var validParameters = new List<string>() { fistName, lastName, date, countryId, type.ToString() };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var countryMock = new Mock<Country>();

            var countryRepositoryMock = new Mock<ICountryRepository>();
            countryRepositoryMock.Setup(c => c.SingleOrDefault(It.IsAny<Expression<Func<Country, bool>>>())).Returns(countryMock.Object);
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);

            var filmStaffRepositoryMock = new Mock<IFilmStaffRepository>();
            dbMock.Setup(d => d.FilmStaffs).Returns(filmStaffRepositoryMock.Object);

            var sut = new CreateFilmStaffCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            factoryMock.Verify(f => f.CreateFilmStaff(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), countryMock.Object, It.IsAny<StaffType>()), Times.Once);
        }

        [TestCase(StaffType.Writer)]
        [TestCase(StaffType.Actor)]
        [TestCase(StaffType.Director)]
        public void CallCreateFilmStaff_WithStaffTypeParameter(StaffType type)
        {
            //Arrange
            var fistName = "firstName";
            var lastName = "lastName";
            var date = "10/10/1980";
            var countryId = "1";
            var validParameters = new List<string>() { fistName, lastName, date, countryId, type.ToString() };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var countryMock = new Mock<Country>();

            var countryRepositoryMock = new Mock<ICountryRepository>();
            countryRepositoryMock.Setup(c => c.SingleOrDefault(It.IsAny<Expression<Func<Country, bool>>>())).Returns(countryMock.Object);
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);

            var filmStaffRepositoryMock = new Mock<IFilmStaffRepository>();
            dbMock.Setup(d => d.FilmStaffs).Returns(filmStaffRepositoryMock.Object);

            var sut = new CreateFilmStaffCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            factoryMock.Verify(f => f.CreateFilmStaff(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<Country>(), type), Times.Once);
        }

        [Test]
        public void CallAddMethodOfDatabaseFilmStaffRepository_WhenStafIsCreated()
        {
            //Arrange
            var fistName = "firstName";
            var lastName = "lastName";
            var date = "10/10/1980";
            var countryId = "1";
            var type = StaffType.Writer;
            var validParameters = new List<string>() { fistName, lastName, date, countryId, type.ToString() };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var filmStafMock = new Mock<FilmStaff>();
            factoryMock.Setup(f => f.CreateFilmStaff(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<Country>(), It.IsAny<StaffType>())).Returns(filmStafMock.Object);

            var countryMock = new Mock<Country>();

            var countryRepositoryMock = new Mock<ICountryRepository>();
            countryRepositoryMock.Setup(c => c.SingleOrDefault(It.IsAny<Expression<Func<Country, bool>>>())).Returns(countryMock.Object);
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);

            var filmStaffRepositoryMock = new Mock<IFilmStaffRepository>();
            dbMock.Setup(d => d.FilmStaffs).Returns(filmStaffRepositoryMock.Object);

            var sut = new CreateFilmStaffCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            filmStaffRepositoryMock.Verify(f => f.Add(filmStafMock.Object), Times.Once);
        }

        [Test]
        public void CallCompleteDatabase_WhenStafIsCreated()
        {
            //Arrange
            var fistName = "firstName";
            var lastName = "lastName";
            var date = "10/10/1980";
            var countryId = "1";
            var type = StaffType.Writer;
            var validParameters = new List<string>() { fistName, lastName, date, countryId, type.ToString() };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var filmStafMock = new Mock<FilmStaff>();
            factoryMock.Setup(f => f.CreateFilmStaff(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<Country>(), It.IsAny<StaffType>())).Returns(filmStafMock.Object);

            var countryMock = new Mock<Country>();

            var countryRepositoryMock = new Mock<ICountryRepository>();
            countryRepositoryMock.Setup(c => c.SingleOrDefault(It.IsAny<Expression<Func<Country, bool>>>())).Returns(countryMock.Object);
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);

            var filmStaffRepositoryMock = new Mock<IFilmStaffRepository>();
            dbMock.Setup(d => d.FilmStaffs).Returns(filmStaffRepositoryMock.Object);

            var sut = new CreateFilmStaffCommand(dbMock.Object, factoryMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            dbMock.Verify(d => d.Complete(), Times.Once);
        }

        [Test]
        public void ReturnFilmStaffCreated_WhenStafIsCreated()
        {
            //Arrange
            var fistName = "firstName";
            var lastName = "lastName";
            var date = "10/10/1980";
            var countryId = "1";
            var type = StaffType.Writer;
            var validParameters = new List<string>() { fistName, lastName, date, countryId, type.ToString() };
            var dbMock = new Mock<IDatabase>();
            var factoryMock = new Mock<IModelsFactory>();

            var filmStafMock = new Mock<FilmStaff>();
            factoryMock.Setup(f => f.CreateFilmStaff(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<Country>(), It.IsAny<StaffType>())).Returns(filmStafMock.Object);

            var countryMock = new Mock<Country>();

            var countryRepositoryMock = new Mock<ICountryRepository>();
            countryRepositoryMock.Setup(c => c.SingleOrDefault(It.IsAny<Expression<Func<Country, bool>>>())).Returns(countryMock.Object);
            dbMock.Setup(d => d.Countries).Returns(countryRepositoryMock.Object);

            var filmStaffRepositoryMock = new Mock<IFilmStaffRepository>();
            dbMock.Setup(d => d.FilmStaffs).Returns(filmStaffRepositoryMock.Object);

            var sut = new CreateFilmStaffCommand(dbMock.Object, factoryMock.Object);
            var expectedResult = "filmStaff created";
            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedResult, result);
        }
    }
}
