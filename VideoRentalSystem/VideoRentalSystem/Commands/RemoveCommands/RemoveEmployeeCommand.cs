using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.RemoveCommands
{
    public class RemoveEmployeeCommand : ICommand
    {
        private readonly IDatabase db;

        public RemoveEmployeeCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 1)
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
                return "Not Valid Employee Id. Fill in numeric value!";
            }

            var employee = this.db.Employees.SingleOrDefault(e => e.Id == employeeId);
            if (employee == null)
            {
                return "Employee with such id doesn't exist!";
            }

            this.db.Employees.Remove(employee);

            this.db.Complete();

            return "Employee removed";
        }
    }
}