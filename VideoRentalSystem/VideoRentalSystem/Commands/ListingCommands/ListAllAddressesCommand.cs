using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.ListingCommands
{
    public class ListAllAddressesCommand : ICommand
    {
        private readonly IDatabase db;

        public ListAllAddressesCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var addresses = this.db.Addesses.GetAll();

            return string.Join("\n", addresses);
        }
    }
}
