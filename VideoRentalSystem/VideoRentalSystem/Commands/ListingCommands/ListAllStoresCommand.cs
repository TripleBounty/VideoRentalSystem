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
            var stores = this.db.Stores.GetAll();

            if (stores.Count == 0)
            {
                return "No Store available";
            }

            return string.Join("\n", stores);
        }
    }
}
