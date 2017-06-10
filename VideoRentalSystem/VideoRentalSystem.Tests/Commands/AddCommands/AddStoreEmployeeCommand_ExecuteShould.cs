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
    public class AddStoreEmployeeCommand_ExecuteShould
    {
        [TestCase("1", "10", "invalid1", "invalid2")]
        [TestCase("1", "10", "invalid1")]
        [TestCase("1")]
        [TestCase()]
        public void ReturnNotValidParameters_WhenParametersAreDifferentOfTwo(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var sut = new AddStoreEmployeeCommand(dbMock.Object);
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
            var sut = new AddStoreEmployeeCommand(dbMock.Object);
            var expectedString = "parameters are empty";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase("invalidNumber", "1")]
        [TestCase("3.7", "1")]
        [TestCase("invalid3", "1")]
        public void ReturnNotValidStoreId_WhenFirstParameterCannotBeParsedToInt(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var sut = new AddStoreEmployeeCommand(dbMock.Object);
            var expectedString = "Not Valid Store Id";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void ReturnSuchIdDoesNotExist_WhenStoreIsNotInTheRepository()
        {
            //Arrange
            var validParameters = new List<string>() { "1", "10" };
            var dbMock = new Mock<IDatabase>();
            var storeRepositoryMock = new Mock<IStoreRepository>();
            storeRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Store, bool>>>())).Returns((Store)null);

            dbMock.Setup(d => d.Stores).Returns(storeRepositoryMock.Object);

            var sut = new AddStoreEmployeeCommand(dbMock.Object);
            var expectedString = "such id doesn't exist";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [TestCase("1", "invalidNumber")]
        [TestCase("2", "3.7")]
        [TestCase("1", "invalid3")]
        public void ReturnNotValidEmployeeId_WhenSecondParameterCannotBeParsedToInt(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();

            var storeMock = new Mock<Store>();
            var storeRepositoryMock = new Mock<IStoreRepository>();
            storeRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Store, bool>>>())).Returns(storeMock.Object);

            dbMock.Setup(d => d.Stores).Returns(storeRepositoryMock.Object);

            var sut = new AddStoreEmployeeCommand(dbMock.Object);
            var expectedString = "Not Valid Employee Id";

            //Act
            var result = sut.Execute(parameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void ReturnSuchIdDoesNotExist_WhenEmployeeIsNotInTheRepository()
        {
            //Arrange
            var validParameters = new List<string>() { "1", "10" };
            var dbMock = new Mock<IDatabase>();

            var storeMock = new Mock<Store>();
            var storeRepositoryMock = new Mock<IStoreRepository>();
            storeRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Store, bool>>>())).Returns(storeMock.Object);

            dbMock.Setup(d => d.Stores).Returns(storeRepositoryMock.Object);

            var employeefRepositoryMock = new Mock<IEmployeesRepository>();
            employeefRepositoryMock.Setup(e => e.SingleOrDefault(It.IsAny<Expression<Func<Employee, bool>>>())).Returns((Employee)null);

            dbMock.Setup(d => d.Employees).Returns(employeefRepositoryMock.Object);

            var sut = new AddStoreEmployeeCommand(dbMock.Object);
            var expectedString = "such id doesn't exist";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void ReturnAlreadyAssignedt_WhenStoreContainsTheEmployee()
        {
            //Arrange
            var validParameters = new List<string>() { "1", "10" };
            var dbMock = new Mock<IDatabase>();

            var storeMock = new Mock<Store>();
            var storeMockOld = new Mock<Store>();
            var storeRepositoryMock = new Mock<IStoreRepository>();
            storeRepositoryMock.SetupSequence(s => s.SingleOrDefault(It.IsAny<Expression<Func<Store, bool>>>())).Returns(storeMock.Object)
                                                                                                                .Returns(storeMockOld.Object);

            dbMock.Setup(d => d.Stores).Returns(storeRepositoryMock.Object);

            var employeeMock = new Mock<Employee>();
            var employeefRepositoryMock = new Mock<IEmployeesRepository>();
            employeefRepositoryMock.Setup(e => e.SingleOrDefault(It.IsAny<Expression<Func<Employee, bool>>>())).Returns(employeeMock.Object);

            dbMock.Setup(d => d.Employees).Returns(employeefRepositoryMock.Object);

            var sut = new AddStoreEmployeeCommand(dbMock.Object);
            var expectedString = "already assigned";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void AddTheEmployeeToTheFilmStaff_WhenStoreDoesNotContainsTheEmployee()
        {
            //Arrange
            var validParameters = new List<string>() { "1", "10" };
            var dbMock = new Mock<IDatabase>();

            var storeMock = new Mock<Store>();
            storeMock.Setup(s => s.Employees).Returns(new List<Employee>());
            var storeMockOld = new Mock<Store>();
            var storeRepositoryMock = new Mock<IStoreRepository>();
            storeRepositoryMock.SetupSequence(s => s.SingleOrDefault(It.IsAny<Expression<Func<Store, bool>>>())).Returns(storeMock.Object)
                                                                                                                .Returns((Store)null);

            dbMock.Setup(d => d.Stores).Returns(storeRepositoryMock.Object);

            var employeeMock = new Mock<Employee>();
            var employeefRepositoryMock = new Mock<IEmployeesRepository>();
            employeefRepositoryMock.Setup(e => e.SingleOrDefault(It.IsAny<Expression<Func<Employee, bool>>>())).Returns(employeeMock.Object);

            dbMock.Setup(d => d.Employees).Returns(employeefRepositoryMock.Object);

            var sut = new AddStoreEmployeeCommand(dbMock.Object);

            //Act
            sut.Execute(validParameters);

            //Assert
            CollectionAssert.Contains(storeMock.Object.Employees, employeeMock.Object);
        }

        [Test]
        public void ReturnEmployeeAdded_WhenStoreDoesNotContainsTheEmployee()
        {
            //Arrange
            var validParameters = new List<string>() { "1", "10" };
            var dbMock = new Mock<IDatabase>();

            var storeMock = new Mock<Store>();
            storeMock.Setup(s => s.Employees).Returns(new List<Employee>());
            var storeMockOld = new Mock<Store>();
            var storeRepositoryMock = new Mock<IStoreRepository>();
            storeRepositoryMock.SetupSequence(s => s.SingleOrDefault(It.IsAny<Expression<Func<Store, bool>>>())).Returns(storeMock.Object)
                                                                                                                .Returns((Store)null);

            dbMock.Setup(d => d.Stores).Returns(storeRepositoryMock.Object);

            var employeeMock = new Mock<Employee>();
            var employeefRepositoryMock = new Mock<IEmployeesRepository>();
            employeefRepositoryMock.Setup(e => e.SingleOrDefault(It.IsAny<Expression<Func<Employee, bool>>>())).Returns(employeeMock.Object);

            dbMock.Setup(d => d.Employees).Returns(employeefRepositoryMock.Object);

            var sut = new AddStoreEmployeeCommand(dbMock.Object);
            var expectedResult = "Employee added";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedResult, result);
        }

        [Test]
        public void CallDbComplete_WhenStoreDoesNotContainsTheEmployee()
        {
            //Arrange
            var validParameters = new List<string>() { "1", "10" };
            var dbMock = new Mock<IDatabase>();

            var storeMock = new Mock<Store>();
            storeMock.Setup(s => s.Employees).Returns(new List<Employee>());
            var storeMockOld = new Mock<Store>();
            var storeRepositoryMock = new Mock<IStoreRepository>();
            storeRepositoryMock.SetupSequence(s => s.SingleOrDefault(It.IsAny<Expression<Func<Store, bool>>>())).Returns(storeMock.Object)
                                                                                                                .Returns((Store)null);

            dbMock.Setup(d => d.Stores).Returns(storeRepositoryMock.Object);

            var employeeMock = new Mock<Employee>();
            var employeefRepositoryMock = new Mock<IEmployeesRepository>();
            employeefRepositoryMock.Setup(e => e.SingleOrDefault(It.IsAny<Expression<Func<Employee, bool>>>())).Returns(employeeMock.Object);

            dbMock.Setup(d => d.Employees).Returns(employeefRepositoryMock.Object);

            var sut = new AddStoreEmployeeCommand(dbMock.Object);
            var expectedResult = "Employee added";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            dbMock.Verify(d => d.Complete(), Times.Once);
        }
    }
}
