using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.UpdateCommands
{
    public class UpdateAddressCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public UpdateAddressCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 4)
            {
                return "Not valid number of parameters";
            }

            if (parameters.Any(x => x == string.Empty))
            {
                return "Some of the passed parameters are empty!";
            }

            int addressId;
            var addressIdParsed = int.TryParse(parameters[0], out addressId);
            if (!addressIdParsed)
            {
                return "Not Valid Address Id. Fill in numeric value!";
            }

            var address = this.db.Addesses.SingleOrDefault(t => t.Id == addressId);
            if (address == null)
            {
                return "Town with such id doesn't exist!";
            }

            var street = parameters[1];
            var postalCode = parameters[2];
            int townId;
            var townIdParsed = int.TryParse(parameters[3], out townId);
            if (!townIdParsed)
            {
                return "Not Valid Town Id. Fill in numeric value!";
            }

            var town = this.db.Towns.SingleOrDefault(t => t.Id == townId);
            if (town == null)
            {
                return "Town with such id doesn't exist!";
            }

            address.Street = street;
            address.PostalCode = postalCode;
            address.Town = town;

            this.db.Complete();

            return "Address updated";
        }
    }
}
