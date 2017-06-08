using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.ListingCommands
{
    public class ListAllEmployeesCommand : ICommand
    {
        private readonly IDatabase db;

        public ListAllEmployeesCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var employees = this.db.Employees.GetAll();

            if (employees.Count == 0)
            {
                return "No Employees available";
            }

            return string.Join("\n", employees);
        }
    }
}