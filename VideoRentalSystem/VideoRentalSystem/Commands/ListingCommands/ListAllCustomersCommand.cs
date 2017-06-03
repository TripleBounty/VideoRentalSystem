using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.ListingCommands
{
    public class ListAllCustomersCommand : ICommand
    {
        private readonly IDatabase db;

        public ListAllCustomersCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var customers = this.db.Customers.GetAll();

            return string.Join("\n", customers);
        }
    }
}