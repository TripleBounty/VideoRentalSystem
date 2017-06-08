using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.ListingCommands
{
    public class ListAllFilmsCommand : ICommand
    {
        private readonly IDatabase db;

        public ListAllFilmsCommand(IDatabase db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var films = this.db.Films.GetAll();

            if (films.Count == 0)
            {
                return "No Films available";
            }

            return string.Join("\n", films);
        }
    }
}