using System;
using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class CreateCountryCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public CreateCountryCommand(IDatabase db, IModelsFactory factory)
        {
            if (db == null)
            {
                throw new ArgumentNullException("CreateCountryCommand db parameter cannot be null");
            }

            this.db = db;

            if (factory == null)
            {
                throw new ArgumentNullException("CreateCountryCommand factory parameter cannot be null");
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

            var countryName = parameters[0];
            var countryCode = parameters[1];

            var country = this.factory.CreateCountry(countryName, countryCode);

            this.db.Countries.Add(country);
            this.db.Complete();

            return "Country created";
        }
    }
}
