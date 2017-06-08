using System;
using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.UpdateCommands
{
    public class UpdateManagerCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public UpdateManagerCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 4)
            {
                return "Not valid number of parameters";
            }

            if (parameters.Any(x => x == string.Empty))
            {
                return "Some of the passed parameters are empty!";
            }

            int managerId;
            var managerIdParsed = int.TryParse(parameters[0], out managerId);
            if (!managerIdParsed)
            {
                return "Not Valid Country Id. Fill in numeric value!";
            }

            var manager = this.db.Employees.SingleOrDefault(c => c.Id == managerId);
            if (manager == null)
            {
                return "Country with such id doesn't exist!";
            }

            var firstName = parameters[1];
            var lastName = parameters[2];

            int salary;
            var slaryParsed = int.TryParse(parameters[3], out salary);
            if (!slaryParsed)
            {
                return "Not Valid Country Id. Fill in numeric value!";
            }
            
            manager.FirstName = firstName;
            manager.LastName = lastName;
            manager.Salary = salary;

            this.db.Complete();

            return "Manager updated";
        }
    }
}