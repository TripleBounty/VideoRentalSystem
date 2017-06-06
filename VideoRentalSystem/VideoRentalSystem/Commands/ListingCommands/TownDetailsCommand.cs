using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.ListingCommands
{
    public class TownDetailsCommand : ICommand
    {
        private readonly IDatabase db;

        public TownDetailsCommand(IDatabase db)
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

            int townId;
            var townIdParsed = int.TryParse(parameters[0], out townId);
            if (!townIdParsed)
            {
                return "Not Valid Town Id. Fill in numeric value!";
            }

            var town = this.db.Towns.SingleOrDefault(t => t.Id == townId);
            if (town == null)
            {
                return "Town with such id doesn't exist!";
            }

            return town.ToString();
        }
    }
}
