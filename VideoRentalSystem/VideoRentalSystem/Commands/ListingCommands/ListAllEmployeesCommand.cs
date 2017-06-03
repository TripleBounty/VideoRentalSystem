using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            return String.Join("\n", employees);
        }
    }
}
