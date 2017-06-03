using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class CreateAddressCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public CreateAddressCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            var street = parameters[0];
            var postalCode = parameters[1];
            var townId = int.Parse(parameters[2]);

            var town = this.db.Towns.SingleOrDefault(t => t.Id == townId);
            var country = town.Country;

            var address = this.factory.CreateAddress(street, postalCode, town, null);

            this.db.Addesses.Add(address);
            this.db.Complete();

            return "Address created";
        }
    }
}
