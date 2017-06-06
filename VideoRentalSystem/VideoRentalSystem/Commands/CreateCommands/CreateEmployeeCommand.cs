using System;
using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class CreateEmployeeCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public CreateEmployeeCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count < 4)
            {
                throw new ArgumentException(string.Format(
                    "Input parameters are not in the correct format!" +
                    Environment.NewLine +
                    "The correct format is : FirstName;LastName;Salary;ManagerId"));
            }

            var firstName = parameters[0];
            var lastName = parameters[1];
            int salary = int.Parse(parameters[2]);
            int managerId = int.Parse(parameters[3]);

            var employeeObj = this.db.Employees.SingleOrDefault(e => e.Id == managerId);

            if (employeeObj == null)
            {
                throw new ArgumentException(string.Format(
                    "The managerId cannot be null or you are trying to get a non existand Manager!"));
            }

            var employee = this.factory.CreateEmployee(firstName, lastName, salary, employeeObj);

            this.db.Employees.Add(employee);
            this.db.Complete();

            return "Employee created";
        }
    }
}