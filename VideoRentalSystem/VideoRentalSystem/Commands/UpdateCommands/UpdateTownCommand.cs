using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.UpdateCommands
{
    public class UpdateTownCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public UpdateTownCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 3)
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

            var townName = parameters[1];
            int countryId;
            var countryIdParsed = int.TryParse(parameters[2], out countryId);
            if (!countryIdParsed)
            {
                return "Not Valid Country Id. Fill in numeric value!";
            }

            var country = this.db.Countries.SingleOrDefault(c => c.Id == countryId);
            if (country == null)
            {
                return "Country with such id doesn't exist!";
            }

            town.Name = townName;
            town.Country = country;

            this.db.Complete();

            return "Town Updated";
        }
    }
}
