using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.AddCommands
{
    public class AddStoreEmployeeCommand : ICommand
    {
        private readonly IDatabase db;

        public AddStoreEmployeeCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 2)
            {
                return "Not valid number of parameters";
            }

            if (parameters.Any(x => x == string.Empty))
            {
                return "Some of the passed parameters are empty!";
            }

            int storeId;
            var storeIdParsed = int.TryParse(parameters[0], out storeId);
            if (!storeIdParsed)
            {
                return "Not Valid Store Id. Fill in numeric value!";
            }

            var store = this.db.Stores.SingleOrDefault(s => s.Id == storeId);
            if (store == null)
            {
                return "Store with such id doesn't exist!";
            }

            int employeeId;
            var employeeIdParsed = int.TryParse(parameters[1], out employeeId);
            if (!employeeIdParsed)
            {
                return "Not Valid Employee Id. Fill in numeric value!";
            }

            var employee = this.db.Employees.SingleOrDefault(e => e.Id == employeeId);
            if (employee == null)
            {
                return "Employee with such id doesn't exist!";
            }

            var storeEmployee = this.db.Stores.SingleOrDefault(s => s.Employees.Any(e => e.Id == employeeId));

            if (storeEmployee != null)
            {
                return $"Employee is already assigned to a store \n {storeEmployee.ToString()}";
            }

            store.Employees.Add(employee);

            this.db.Complete();

            return "Employee added";
        }
    }
}
