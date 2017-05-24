using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands
{
    public class CreateEmployeesCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public CreateEmployeesCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            int salary = int.Parse(parameters[2]);
            int managerId = int.Parse(parameters[3]);
            var employee = this.factory.CreateEmployees(parameters[0], parameters[1], salary, managerId) as Employees;

            db.Employees.CreateEmployees(employee);
            db.Complete();

            return "Employee created";
        }
    }
}
