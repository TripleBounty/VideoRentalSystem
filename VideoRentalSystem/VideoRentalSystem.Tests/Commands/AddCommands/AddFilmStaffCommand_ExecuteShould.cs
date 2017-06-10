using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VideoRentalSystem.Commands.AddCommands;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Tests.Commands.AddCommands
{
    [TestFixture]
    public class AddFilmStaffCommand_ExecuteShould
    {
        [TestCase("1", "10", "invalid1", "invalid2")]
        [TestCase("1", "10", "invalid1")]
        [TestCase("1")]
        [TestCase()]
        public void ReturnNotValidParameters_WhenParametersAreDifferentOfTwo(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var sut = new AddFilmStaffCommand(dbMock.Object);
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
            var sut = new AddFilmStaffCommand(dbMock.Object);
            var expectedString = "parameters are empty";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase("invalidNumber", "1")]
        [TestCase("3.7", "1")]
        [TestCase("invalid3", "1")]
        public void ReturnNotValidFilmId_WhenFirstParameterCannotBeParsedToInt(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var sut = new AddFilmStaffCommand(dbMock.Object);
            var expectedString = "Not Valid Film Id";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void ReturnSuchIdDoesNotExist_WhenFilmIsNotInTheRepository()
        {
            //Arrange
            var validParameters = new List<string>() { "1", "10" };
            var dbMock = new Mock<IDatabase>();
            var filmRepositoryMock = new Mock<IFilmRepository>();
            filmRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Film, bool>>>())).Returns((Film)null);

            dbMock.Setup(d => d.Films).Returns(filmRepositoryMock.Object);

            var sut = new AddFilmStaffCommand(dbMock.Object);
            var expectedString = "such id doesn't exist";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase("1", "invalidNumber")]
        [TestCase("2", "3.7")]
        [TestCase("1", "invalid3")]
        public void ReturnNotValidFilmStaffId_WhenSecondParameterCannotBeParsedToInt(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();

            var filmMock = new Mock<Film>();
            var filmRepositoryMock = new Mock<IFilmRepository>();
            filmRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<Film, bool>>>())).Returns(filmMock.Object);

            dbMock.Setup(d => d.Films).Returns(filmRepositoryMock.Object);

            var sut = new AddFilmStaffCommand(dbMock.Object);
            var expectedString = "Not Valid Staff Id";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void ReturnSuchIdDoesNotExist_WhenFilmStaffIsNotInTheRepository()
        {
            //Arrange
            var validParameters = new List<string>() { "1", "10" };
            var dbMock = new Mock<IDatabase>();

            var filmMock = new Mock<Film>();
            var filmRepositoryMock = new Mock<IFilmRepository>();
            filmRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<Film, bool>>>())).Returns(filmMock.Object);

            dbMock.Setup(d => d.Films).Returns(filmRepositoryMock.Object);

            var filmStaffRepositoryMock = new Mock<IFilmStaffRepository>();
            filmStaffRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<FilmStaff, bool>>>())).Returns((FilmStaff)null);

            dbMock.Setup(d => d.FilmStaffs).Returns(filmStaffRepositoryMock.Object);

            var sut = new AddFilmStaffCommand(dbMock.Object);
            var expectedString = "such id doesn't exist";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void ReturnAlreadyAssignedt_WhenFilmStaffContainsTheNewFilm()
        {
            //Arrange
            var validFilmId = 1;
            var validParameters = new List<string>() { validFilmId.ToString(), "10" };
            var dbMock = new Mock<IDatabase>();

            var filmMock = new Mock<Film>();
            filmMock.Object.Id = validFilmId;
            var filmRepositoryMock = new Mock<IFilmRepository>();
            filmRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<Film, bool>>>())).Returns(filmMock.Object);

            dbMock.Setup(d => d.Films).Returns(filmRepositoryMock.Object);

            var filmStaffRepositoryMock = new Mock<IFilmStaffRepository>();

            var filmStaffMock = new Mock<FilmStaff>();

            var films = new List<Film>() { filmMock.Object };
            filmStaffMock.Setup(f => f.Films).Returns(films);

            filmStaffRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<FilmStaff, bool>>>())).Returns(filmStaffMock.Object);

            dbMock.Setup(d => d.FilmStaffs).Returns(filmStaffRepositoryMock.Object);

            var sut = new AddFilmStaffCommand(dbMock.Object);
            var expectedString = "already assigned";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void AddTheFilmToTheFilmStaff_WhenFilmStaffDoesNotContainsTheNewFilm()
        {
            //Arrange
            var validFilmId = 1;
            var validParameters = new List<string>() { validFilmId.ToString(), "10" };
            var dbMock = new Mock<IDatabase>();

            var filmMock = new Mock<Film>();
            filmMock.Object.Id = validFilmId;
            var filmRepositoryMock = new Mock<IFilmRepository>();
            filmRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<Film, bool>>>())).Returns(filmMock.Object);

            dbMock.Setup(d => d.Films).Returns(filmRepositoryMock.Object);

            var filmStaffRepositoryMock = new Mock<IFilmStaffRepository>();

            var filmStaffMock = new Mock<FilmStaff>();

            var filmMockOld = new Mock<Film>();
            filmMockOld.Object.Id = 2;

            var films = new List<Film>() { filmMockOld.Object };
            filmStaffMock.Setup(f => f.Films).Returns(films);

            filmStaffRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<FilmStaff, bool>>>())).Returns(filmStaffMock.Object);

            dbMock.Setup(d => d.FilmStaffs).Returns(filmStaffRepositoryMock.Object);

            var sut = new AddFilmStaffCommand(dbMock.Object);

            //Act
            sut.Execute(validParameters);

            //Assert
            CollectionAssert.Contains(filmStaffMock.Object.Films, filmMock.Object);
        }

        [Test]
        public void ReturnFilmAdded_WhenFilmStaffDoesNotContainsTheNewFilm()
        {
            //Arrange
            var validFilmId = 1;
            var validParameters = new List<string>() { validFilmId.ToString(), "10" };
            var dbMock = new Mock<IDatabase>();

            var filmMock = new Mock<Film>();
            filmMock.Object.Id = validFilmId;
            var filmRepositoryMock = new Mock<IFilmRepository>();
            filmRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<Film, bool>>>())).Returns(filmMock.Object);

            dbMock.Setup(d => d.Films).Returns(filmRepositoryMock.Object);

            var filmStaffRepositoryMock = new Mock<IFilmStaffRepository>();

            var filmStaffMock = new Mock<FilmStaff>();

            var filmMockOld = new Mock<Film>();
            filmMockOld.Object.Id = 2;

            var films = new List<Film>() { filmMockOld.Object };
            filmStaffMock.Setup(f => f.Films).Returns(films);

            filmStaffRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<FilmStaff, bool>>>())).Returns(filmStaffMock.Object);

            dbMock.Setup(d => d.FilmStaffs).Returns(filmStaffRepositoryMock.Object);

            var sut = new AddFilmStaffCommand(dbMock.Object);
            var expectedResult = "Film added";
            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedResult, result);
        }

        [Test]
        public void CallDbComplete_WhenFilmStaffDoesNotContainsTheNewFilm()
        {
            //Arrange
            var validFilmId = 1;
            var validParameters = new List<string>() { validFilmId.ToString(), "10" };
            var dbMock = new Mock<IDatabase>();

            var filmMock = new Mock<Film>();
            filmMock.Object.Id = validFilmId;
            var filmRepositoryMock = new Mock<IFilmRepository>();
            filmRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<Film, bool>>>())).Returns(filmMock.Object);

            dbMock.Setup(d => d.Films).Returns(filmRepositoryMock.Object);

            var filmStaffRepositoryMock = new Mock<IFilmStaffRepository>();

            var filmStaffMock = new Mock<FilmStaff>();

            var filmMockOld = new Mock<Film>();
            filmMockOld.Object.Id = 2;

            var films = new List<Film>() { filmMockOld.Object };
            filmStaffMock.Setup(f => f.Films).Returns(films);

            filmStaffRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<FilmStaff, bool>>>())).Returns(filmStaffMock.Object);

            dbMock.Setup(d => d.FilmStaffs).Returns(filmStaffRepositoryMock.Object);

            var sut = new AddFilmStaffCommand(dbMock.Object);

            //Act
            sut.Execute(validParameters);

            //Assert
            dbMock.Verify(d => d.Complete(), Times.Once);
        }
    }
}
