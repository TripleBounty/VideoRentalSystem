using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using VideoRentalSystem.Commands.RemoveCommands;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Tests.Commands.AddCommands
{
    [TestFixture]
    public class RemoveStoreEmployeeCommand_ExecuteShould
    {
        [TestCase("1", "10", "invalid1", "invalid2")]
        [TestCase("1", "10", "invalid1")]
        [TestCase("1")]
        [TestCase()]
        public void ReturnNotValidParameters_WhenParametersAreDifferentOfTwo(params string[] parameters)
        {
            //Arrange
            var dbMock = new Mock<IDatabase>();
            var sut = new RemoveStoreEmployeeCommand(dbMock.Object);
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
            var sut = new RemoveStoreEmployeeCommand(dbMock.Object);
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
            var sut = new RemoveStoreEmployeeCommand(dbMock.Object);
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

            var sut = new RemoveStoreEmployeeCommand(dbMock.Object);
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

            var sut = new RemoveStoreEmployeeCommand(dbMock.Object);
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

            var sut = new RemoveStoreEmployeeCommand(dbMock.Object);
            var expectedString = "such id doesn't exist";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void ReturnNotAssignedToTheStore_WhenStoreNotContainsTheEmployee()
        {
            //Arrange
            var validParameters = new List<string>() { "1", "10" };
            var dbMock = new Mock<IDatabase>();

            var storeMock = new Mock<Store>();

            storeMock.Setup(s => s.Employees.Contains(It.IsAny<Employee>())).Returns(false);

            var storeRepositoryMock = new Mock<IStoreRepository>();
            storeRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Store, bool>>>())).Returns(storeMock.Object);

            dbMock.Setup(d => d.Stores).Returns(storeRepositoryMock.Object);

            var employeeMock = new Mock<Employee>();
            var employeefRepositoryMock = new Mock<IEmployeesRepository>();
            employeefRepositoryMock.Setup(e => e.SingleOrDefault(It.IsAny<Expression<Func<Employee, bool>>>())).Returns(employeeMock.Object);

            dbMock.Setup(d => d.Employees).Returns(employeefRepositoryMock.Object);

            var sut = new RemoveStoreEmployeeCommand(dbMock.Object);
            var expectedString = "not assigned to the store";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedString, result);
        }

        [Test]
        public void RemoveTheEmployeeToTheStore_WhenStoreContainsTheEmployee()
        {
            //Arrange
            var validParameters = new List<string>() { "1", "10" };
            var dbMock = new Mock<IDatabase>();

            var storeMock = new Mock<Store>();

            var storeRepositoryMock = new Mock<IStoreRepository>();
            storeRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Store, bool>>>())).Returns(storeMock.Object);

            dbMock.Setup(d => d.Stores).Returns(storeRepositoryMock.Object);

            var employeeMock = new Mock<Employee>();
            var employeefRepositoryMock = new Mock<IEmployeesRepository>();
            employeefRepositoryMock.Setup(e => e.SingleOrDefault(It.IsAny<Expression<Func<Employee, bool>>>())).Returns(employeeMock.Object);

            dbMock.Setup(d => d.Employees).Returns(employeefRepositoryMock.Object);

            var employeeList = new List<Employee>() { employeeMock.Object };
            storeMock.Setup(s => s.Employees).Returns(employeeList);

            var sut = new RemoveStoreEmployeeCommand(dbMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            CollectionAssert.DoesNotContain(storeMock.Object.Employees, employeeMock.Object);
        }

        [Test]
        public void ReturnEmployeeRemoved_WhenStoreContainsTheEmployee()
        {
            var validParameters = new List<string>() { "1", "10" };
            var dbMock = new Mock<IDatabase>();

            var storeMock = new Mock<Store>();

            var storeRepositoryMock = new Mock<IStoreRepository>();
            storeRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Store, bool>>>())).Returns(storeMock.Object);

            dbMock.Setup(d => d.Stores).Returns(storeRepositoryMock.Object);

            var employeeMock = new Mock<Employee>();
            var employeefRepositoryMock = new Mock<IEmployeesRepository>();
            employeefRepositoryMock.Setup(e => e.SingleOrDefault(It.IsAny<Expression<Func<Employee, bool>>>())).Returns(employeeMock.Object);

            dbMock.Setup(d => d.Employees).Returns(employeefRepositoryMock.Object);

            var employeeList = new List<Employee>() { employeeMock.Object };
            storeMock.Setup(s => s.Employees).Returns(employeeList);

            var sut = new RemoveStoreEmployeeCommand(dbMock.Object);
            var expectedResult = "Employee removed";

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            StringAssert.Contains(expectedResult, result);
        }

        [Test]
        public void CallDbComplete_WhenTheEmployeeIsRemoved()
        {
            var validParameters = new List<string>() { "1", "10" };
            var dbMock = new Mock<IDatabase>();

            var storeMock = new Mock<Store>();

            var storeRepositoryMock = new Mock<IStoreRepository>();
            storeRepositoryMock.Setup(s => s.SingleOrDefault(It.IsAny<Expression<Func<Store, bool>>>())).Returns(storeMock.Object);

            dbMock.Setup(d => d.Stores).Returns(storeRepositoryMock.Object);

            var employeeMock = new Mock<Employee>();
            var employeefRepositoryMock = new Mock<IEmployeesRepository>();
            employeefRepositoryMock.Setup(e => e.SingleOrDefault(It.IsAny<Expression<Func<Employee, bool>>>())).Returns(employeeMock.Object);

            dbMock.Setup(d => d.Employees).Returns(employeefRepositoryMock.Object);

            var employeeList = new List<Employee>() { employeeMock.Object };
            storeMock.Setup(s => s.Employees).Returns(employeeList);

            var sut = new RemoveStoreEmployeeCommand(dbMock.Object);

            //Act
            var result = sut.Execute(validParameters);

            //Assert
            dbMock.Verify(d => d.Complete(), Times.Once);
        }
    }
}
