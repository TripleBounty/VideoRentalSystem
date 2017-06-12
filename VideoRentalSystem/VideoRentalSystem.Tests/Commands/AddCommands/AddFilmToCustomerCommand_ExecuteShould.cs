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
    public class AddFilmToCustomerCommand_ExecuteShould
    {
        [TestCase("1", "10", "invalid1", "invalid2")]
        [TestCase("1", "10", "invalid1")]
        [TestCase("1")]
        [TestCase()]
        public void ReturnNotValidParameters_WhenParametersAreDifferentOfTwo(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var sut = new AddFilmToCustomerCommand(dbMock.Object);
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
            var sut = new AddFilmToCustomerCommand(dbMock.Object);
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
            var sut = new AddFilmToCustomerCommand(dbMock.Object);
            var expectedString = "Fill in numeric value";

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

            var sut = new AddFilmToCustomerCommand(dbMock.Object);
            var expectedString = "Film not found";

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

            var filmMock = new Mock<Film>();
            var filmRepositoryMock = new Mock<IFilmRepository>();
            filmRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<Film, bool>>>())).Returns(filmMock.Object);

            dbMock.Setup(d => d.Films).Returns(filmRepositoryMock.Object);

            var sut = new AddFilmToCustomerCommand(dbMock.Object);
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

            var filmMock = new Mock<Film>();
            var filmRepositoryMock = new Mock<IFilmRepository>();
            filmRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<Film, bool>>>())).Returns(filmMock.Object);

            dbMock.Setup(d => d.Films).Returns(filmRepositoryMock.Object);

            var CustomerRepositoryMock = new Mock<ICustomerRepository>();
            CustomerRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<Customer, bool>>>())).Returns((Customer)null);

            dbMock.Setup(d => d.Customers).Returns(CustomerRepositoryMock.Object);

            var sut = new AddFilmToCustomerCommand(dbMock.Object);

            var expectedString = "Customer not found";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void AddTheFilmToTheCustomer_WhenFilmDoesNotContainsTheNewFilm()
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

            var CustomerRepositoryMock = new Mock<ICustomerRepository>();

            var CustomerMock = new Mock<Customer>();

            var filmMockOld = new Mock<Film>();
            filmMockOld.Object.Id = 2;

            var films = new List<Film>() { filmMockOld.Object };
            CustomerMock.Setup(f => f.Films).Returns(films);

            CustomerRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(CustomerMock.Object);

            dbMock.Setup(d => d.Customers).Returns(CustomerRepositoryMock.Object);

            var sut = new AddFilmToCustomerCommand(dbMock.Object);

            //Act
            sut.Execute(validParameters);

            //Assert
            CollectionAssert.Contains(CustomerMock.Object.Films, filmMock.Object);
        }

        [Test]
        public void ReturnFilmAdded_WhenCustomerDoesNotContainsTheNewFilm()
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

            var CustomerRepositoryMock = new Mock<ICustomerRepository>();

            var CustomerMock = new Mock<Customer>();

            var filmMockOld = new Mock<Film>();
            filmMockOld.Object.Id = 2;

            var films = new List<Film>() { filmMockOld.Object };
            CustomerMock.Setup(f => f.Films).Returns(films);

            CustomerRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(CustomerMock.Object);

            dbMock.Setup(d => d.Customers).Returns(CustomerRepositoryMock.Object);

            var sut = new AddFilmToCustomerCommand(dbMock.Object);
            var expectedResult = "added to";
            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedResult, result);
        }

        [Test]
        public void CallDbComplete_WhenCustomerDoesNotContainsTheNewFilm()
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

            var CustomerRepositoryMock = new Mock<ICustomerRepository>();

            var CustomerMock = new Mock<Customer>();

            var filmMockOld = new Mock<Film>();
            filmMockOld.Object.Id = 2;

            var films = new List<Film>() { filmMockOld.Object };
            CustomerMock.Setup(f => f.Films).Returns(films);

            CustomerRepositoryMock.Setup(f => f.SingleOrDefault(It.IsAny<Expression<Func<Customer, bool>>>())).Returns(CustomerMock.Object);

            dbMock.Setup(d => d.Customers).Returns(CustomerRepositoryMock.Object);

            var sut = new AddFilmToCustomerCommand(dbMock.Object);

            //Act
            sut.Execute(validParameters);

            //Assert
            dbMock.Verify(d => d.Complete(), Times.Once);
        }
    }
}
