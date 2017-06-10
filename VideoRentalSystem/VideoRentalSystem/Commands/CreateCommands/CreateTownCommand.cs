using System;
using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class CreateTownCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public CreateTownCommand(IDatabase db, IModelsFactory factory)
        {
            if (db == null)
            {
                throw new ArgumentNullException("CreateTarifCommand db parameter cannot be null");
            }

            this.db = db;

            if (factory == null)
            {
                throw new ArgumentNullException("CreateTarifCommand factory parameter cannot be null");
            }

            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 2)
            {
                return "Not valid number of parameters";
            }

            if (parameters.Any(x => x == string.Empty))
            {
                return "Some of the passed parameters are empty!";
            }

            var townName = parameters[0];
            int countryId;
            var countryIdParsed = int.TryParse(parameters[1], out countryId);
            if (!countryIdParsed)
            {
                return "Not Valid Country Id. Fill in numeric value!";
            }

            var country = this.db.Countries.SingleOrDefault(c => c.Id == countryId);
            if (country == null)
            {
                return "Country with such id doesn't exist!";
            }

            var town = this.factory.CreateTown(townName, country);

            this.db.Towns.Add(town);
            this.db.Complete();

            return "Town created";
        }
    }
}
