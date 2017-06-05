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
            var countries = this.db.Countries.GetAll();
            if (countries.Count == 0)
            {
                return "No Country available";
            }

            return string.Join("\n", countries);
        }
    }
}
