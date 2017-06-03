using System.Collections.Generic;
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
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            var townName = parameters[0];
            var countryId = int.Parse(parameters[1]);

            var country = this.db.Countries.SingleOrDefault(c => c.Id == countryId);

            var town = this.factory.CreateTown(townName, country);

            this.db.Towns.Add(town);
            this.db.Complete();

            return "Town created";
        }
    }
}
