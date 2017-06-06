using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Postgre.Contracts;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Commands.ListingCommands
{
    public class ListAllTarifsByTypeCommand : ICommand
    {
        private readonly IDatabasePostgre db;

        public ListAllTarifsByTypeCommand(IDatabasePostgre db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 1)
            {
                return "Not valid number of parameters";
            }

            if (parameters.Any(x => x == string.Empty))
            {
                return "Some of the passed parameters are empty!";
            }

            bool active = parameters[0].ToUpper().Equals("Y") ? false : true;

            var tarifs = this.db.Tarifs.Find(t => t.IsDeleted == active);

            if (tarifs.Count<Tarif>() == 0)
            {
                return "No Tarif available";
            }

            return string.Join("\n", tarifs);
        }
    }
}
