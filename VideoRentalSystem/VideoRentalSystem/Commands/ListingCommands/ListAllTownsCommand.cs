using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.ListingCommands
{
    public class ListAllTownsCommand : ICommand
    {
        private readonly IDatabase db;

        public ListAllTownsCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var towns = this.db.Towns.GetAll();

            return string.Join("\n", towns);
        }
    }
}
