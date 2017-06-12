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
    public class AddGenreToCustomerCommand_ExecuteShould
    {
        [TestCase("1", "10", "invalid1", "invalid2")]
        [TestCase("1", "10", "invalid1")]
        [TestCase("1")]
        [TestCase()]
        public void ReturnNotValidParameters_WhenParametersAreDifferentOfTwo(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var sut = new  AddGenreToCustomerCommand(dbMock.Object);
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
            var sut = new  AddGenreToCustomerCommand(dbMock.Object);
            var expectedString = "parameters are empty";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase("invalidNumber", "1")]
        [TestCase("3.7", "1")]
        [TestCase("invalid3", "1")]
        public void ReturnNotValidGenreId_WhenFirstParameterCannotBeParsedToInt(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var sut = new  AddGenreToCustomerCommand(dbMock.Object);
            var expectedString = "Fill in numeric value";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void ReturnSuchIdDoesNotExist_WhenGenreIsNotInTheRepository()
        {
            //Arrange
            var validParameters = new List<string>() { "1", "10" };
            var dbMock = new Mock<IDatabase>();
            var genreRepositoryMock = new Mock<IFilmGenreRepository>();
            genreRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<FilmGenre, bool>>>())).Returns((FilmGenre)null);

            dbMock.Setup(d => d.FilmGenres).Returns(genreRepositoryMock.Object);

            var sut = new  AddGenreToCustomerCommand(dbMock.Object);
            var expectedString = "Genre not found";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase("1", "invalidNumber")]
        [TestCase("2", "3.7")]
        [TestCase("1", "invalid3")]
        public void ReturnNotValidCustomerId_WhenSecondParameterCannotBeParsedToInt(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var genreMock = new Mock<FilmGenre>();
            var genreRepositoryMock = new Mock<IFilmGenreRepository>();
            genreRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<FilmGenre, bool>>>())).Returns(genreMock.Object);
            dbMock.Setup(d => d.FilmGenres).Returns(genreRepositoryMock.Object);

            var filmMock = new Mock<Film>();
            var filmRepositoryMock = new Mock<IFilmRepository>();
            filmRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<Film, bool>>>())).Returns(filmMock.Object);

            dbMock.Setup(d => d.Films).Returns(filmRepositoryMock.Object);

            var sut = new  AddGenreToCustomerCommand(dbMock.Object);
            var expectedString = "Fill in numeric value";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void ReturnSuchIdDoesNotExist_WhenCustomerIsNotFoundInTheRepository()
        {
            //Arrange
            var validParameters = new List<string>() { "1", "10" };
            var dbMock = new Mock<IDatabase>();

            var genreMock = new Mock<FilmGenre>();
            var genreRepositoryMock = new Mock<IFilmGenreRepository>();
            genreRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<FilmGenre, bool>>>())).Returns(genreMock.Object);

            dbMock.Setup(d => d.FilmGenres).Returns(genreRepositoryMock.Object);

            var CustomerRepositoryMock = new Mock<ICustomerRepository>();
            CustomerRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<Customer, bool>>>())).Returns((Customer)null);

            dbMock.Setup(d => d.Customers).Returns(CustomerRepositoryMock.Object);

            var sut = new  AddGenreToCustomerCommand(dbMock.Object);

            var expectedString = "Customer not found";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void AddTheGenreToTheCustomer_WhenCustomerDoesNotContainsTheNewGenre()
        {
            //Arrange
            var validGenreId = 1;
            var validParameters = new List<string>() { validGenreId.ToString(), "10" };
            var dbMock = new Mock<IDatabase>();

            var genreMock = new Mock<FilmGenre>();
            genreMock.Object.Id = validGenreId;
            var genreRepositoryMock = new Mock<IFilmGenreRepository>();
            genreRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<FilmGenre, bool>>>())).Returns(genreMock.Object);

            dbMock.Setup(d => d.FilmGenres).Returns(genreRepositoryMock.Object);

            var CustomerRepositoryMock = new Mock<ICustomerRepository>();

            var CustomerMock = new Mock<Customer>();

            var genreMockOld = new Mock<FilmGenre>();
            genreMockOld.Object.Id = 1;

            var genres = new List<FilmGenre>() { genreMockOld.Object };
            CustomerMock.Setup(f => f.Genres).Returns(genres);

            CustomerRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(CustomerMock.Object);

            dbMock.Setup(d => d.Customers).Returns(CustomerRepositoryMock.Object);

            var sut = new  AddGenreToCustomerCommand(dbMock.Object);

            //Act
            sut.Execute(validParameters);

            //Assert
            CollectionAssert.Contains(CustomerMock.Object.Genres, genreMock.Object);
        }

        [Test]
        public void ReturnGenreAdded_WhenCustomerDoesNotContainsTheNewGenre()
        {
            //Arrange
            var validGenreId = 1;
            var validParameters = new List<string>() { validGenreId.ToString(), "10" };
            var dbMock = new Mock<IDatabase>();

            var genreMock = new Mock<FilmGenre>();
            genreMock.Object.Id = validGenreId;
            var genreRepositoryMock = new Mock<IFilmGenreRepository>();
            genreRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<FilmGenre, bool>>>())).Returns(genreMock.Object);

            dbMock.Setup(d => d.FilmGenres).Returns(genreRepositoryMock.Object);

            var CustomerRepositoryMock = new Mock<ICustomerRepository>();

            var CustomerMock = new Mock<Customer>();

            var filmMockOld = new Mock<Film>();
            filmMockOld.Object.Id = 2;

            var films = new List<Film>() { filmMockOld.Object };
            CustomerMock.Setup(f => f.Films).Returns(films);

            CustomerRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(CustomerMock.Object);

            dbMock.Setup(d => d.Customers).Returns(CustomerRepositoryMock.Object);

            var sut = new  AddGenreToCustomerCommand(dbMock.Object);
            var expectedResult = "added to";
            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedResult, result);
        }

        [Test]
        public void CallDbComplete_WhenCustomerDoesNotContainsTheNewGenre()
        {
            //Arrange
            var validGenreId = 1;
            var validParameters = new List<string>() { validGenreId.ToString(), "10" };
            var dbMock = new Mock<IDatabase>();

            var genreMock = new Mock<FilmGenre>();
            genreMock.Object.Id = validGenreId;
            var genreRepositoryMock = new Mock<IFilmGenreRepository>();
            genreRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<FilmGenre, bool>>>())).Returns(genreMock.Object);

            dbMock.Setup(d => d.FilmGenres).Returns(genreRepositoryMock.Object);

            var CustomerRepositoryMock = new Mock<ICustomerRepository>();

            var CustomerMock = new Mock<Customer>();

            var filmMockOld = new Mock<Film>();
            filmMockOld.Object.Id = 2;

            var films = new List<Film>() { filmMockOld.Object };
            CustomerMock.Setup(f => f.Films).Returns(films);

            CustomerRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(CustomerMock.Object);

            dbMock.Setup(d => d.Customers).Returns(CustomerRepositoryMock.Object);

            var sut = new  AddGenreToCustomerCommand(dbMock.Object);

            //Act
            sut.Execute(validParameters);

            //Assert
            dbMock.Verify(d => d.Complete(), Times.Once);
        }
    }
}
