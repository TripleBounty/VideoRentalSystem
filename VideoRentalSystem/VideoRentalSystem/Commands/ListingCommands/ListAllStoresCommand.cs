using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.ListingCommands
{
    public class ListAllStoresCommand : ICommand
    {
        private readonly IDatabase db;

        public ListAllStoresCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var towns = this.db.Stores.GetAll();

            return string.Join("\n", towns);
        }
    }
}
