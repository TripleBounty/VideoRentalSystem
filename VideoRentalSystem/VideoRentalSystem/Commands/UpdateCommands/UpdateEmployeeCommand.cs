using System;
using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.UpdateCommands
{
    public class UpdateEmployeeCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public UpdateEmployeeCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 5)
            {
                return "Not valid number of parameters";
            }

            if (parameters.Any(x => x == string.Empty))
            {
                return "Some of the passed parameters are empty!";
            }            
            
            int employeeId;
            var employeeIdParsed = int.TryParse(parameters[0], out employeeId);
            if (!employeeIdParsed)
            {
                return "Not Valid Country Id. Fill in numeric value!";
            }

            var employee = this.db.Employees.SingleOrDefault(c => c.Id == employeeId);
            if (employee == null)
            {
                return "Country with such id doesn't exist!";
            }

            var firstName = parameters[1];
            var lastName = parameters[2];

            int salary;
            int managerId;

            try
            {
                salary = int.Parse(parameters[3]);
                managerId = int.Parse(parameters[4]);
            }
            catch
            {
                throw new ArgumentException(string.Format(
                    "Input parameters are not in the correct format!" +
                    Environment.NewLine +
                    "The correct format is: FirstName string;LastName string;Salary int; ManagerId int"));
            }

            var employeeObj = this.db.Employees.SingleOrDefault(e => e.Id == managerId);
            if (employeeObj == null)
            {
                throw new ArgumentException(string.Format(
                    "The managerId cannot be null or you are trying to get a non existand Manager!"));
            }

            employee.FirstName = firstName;
            employee.LastName = lastName;
            employee.Salary = salary;
            employee.Manager = employeeObj;

            this.db.Complete();

            return "Employee updated";
        }
    }
}