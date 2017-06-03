using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.ListingCommands
{
    public class ListAllCountriesCommand : ICommand
    {
        private readonly IDatabase db;

        public ListAllCountriesCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var contries = this.db.Countries.GetAll();

            return string.Join("\n", contries);
        }
    }
}
