using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.RemoveCommands
{
    public class RemoveManagerCommand : ICommand
    {
        private readonly IDatabase db;

        public RemoveManagerCommand(IDatabase db)
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

            int managerId;
            var managerIdParsed = int.TryParse(parameters[0], out managerId);
            if (!managerIdParsed)
            {
                return "Not Valid Manager Id. Fill in numeric value!";
            }

            var manager = this.db.Employees.SingleOrDefault(e => e.Id == managerId);
            if (manager == null)
            {
                return "Manager with such id doesn't exist!";
            }

            this.db.Employees.Remove(manager);

            this.db.Complete();

            return "Manager removed";
        }
    }
}