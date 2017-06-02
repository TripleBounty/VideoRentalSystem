﻿using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands
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
            var firstName = parameters[0];
            var lastName = parameters[1];
            int salary = int.Parse(parameters[2]);
            int managerId = int.Parse(parameters[3]);

            var employee = this.factory.CreateEmployee(firstName, lastName, salary, managerId);

            db.Employees.Add(employee);
            db.Complete();

            return "Employee created";
        }
    }
}
