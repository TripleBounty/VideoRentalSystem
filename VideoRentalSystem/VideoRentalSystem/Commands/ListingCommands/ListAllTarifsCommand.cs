using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Postgre.Contracts;

namespace VideoRentalSystem.Commands.ListingCommands
{
    public class ListAllTarifsCommand : ICommand
    {
        private readonly IDatabasePostgre db;

        public ListAllTarifsCommand(IDatabasePostgre db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var tarifs = this.db.Tarifs.GetAll();
            if (tarifs.Count == 0)
            {
                return "No Tarif available";
            }

            return string.Join("\n", tarifs);
        }
    }
}
