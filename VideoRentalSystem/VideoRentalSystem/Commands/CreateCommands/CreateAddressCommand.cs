using System.Collections.Generic;
using System.Linq;
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
            if (parameters.Count != 3)
            {
                return "Not valid number of parameters";
            }

            if (parameters.Any(x => x == string.Empty))
            {
                return "Some of the passed parameters are empty!";
            }

            var street = parameters[0];
            var postalCode = parameters[1];
            int townId;
            var townIdParsed = int.TryParse(parameters[2], out townId);
            if (!townIdParsed)
            {
                return "Not Valid Town Id. Fill in numeric value!";
            }

            var town = this.db.Towns.SingleOrDefault(t => t.Id == townId);
            if (town == null)
            {
                return "Town with such id doesn't exist!";
            }

            var country = town.Country;

            var address = this.factory.CreateAddress(street, postalCode, town, country);

            this.db.Addesses.Add(address);
            this.db.Complete();

            return "Address created";
        }
    }
}
