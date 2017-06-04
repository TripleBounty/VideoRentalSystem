using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.ListingCommands
{
    public class ListAllStoragesCommand : ICommand
    {
        private readonly IDatabase db;

        public ListAllStoragesCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var storages = this.db.Storages.GetAll();

            return string.Join("\n", storages);
        }
    }
}
