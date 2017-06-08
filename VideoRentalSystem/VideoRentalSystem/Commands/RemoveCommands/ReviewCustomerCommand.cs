using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.RemoveCommands
{
    public class RemoveCustomerCommand : ICommand
    {
        private readonly IDatabase db;

        public RemoveCustomerCommand(IDatabase db)
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

            int customerId;
            var customerIdParsed = int.TryParse(parameters[0], out customerId);
            if (!customerIdParsed)
            {
                return "Not Valid Customer Id. Fill in numeric value!";
            }

            var customer = this.db.Customers.SingleOrDefault(e => e.Id == customerId);
            if (customer == null)
            {
                return "Customer with such id doesn't exist!";
            }

            this.db.Customers.Remove(customer);

            this.db.Complete();

            return "Customer removed";
        }
    }
}