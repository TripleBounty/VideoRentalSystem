using System.Collections.Generic;
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
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            var countryName = parameters[0];
            var countryCode = parameters[1];

            var country = this.factory.CreateCountry(countryName, countryCode);

            this.db.Countries.Add(country);
            this.db.Complete();

            return "Country created";
        }
    }
}
