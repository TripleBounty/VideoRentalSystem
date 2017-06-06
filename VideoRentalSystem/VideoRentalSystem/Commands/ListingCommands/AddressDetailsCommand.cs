using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.ListingCommands
{
    public class AddressDetailsCommand : ICommand
    {
        private readonly IDatabase db;

        public AddressDetailsCommand(IDatabase db)
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

            int addressId;
            var addressIdParsed = int.TryParse(parameters[0], out addressId);
            if (!addressIdParsed)
            {
                return "Not Valid Town Id. Fill in numeric value!";
            }

            var address = this.db.Addesses.SingleOrDefault(a => a.Id == addressId);
            if (address == null)
            {
                return "Address with such id doesn't exist!";
            }

            return address.ToString();
        }
    }
}
