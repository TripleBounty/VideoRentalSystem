using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.ListingCommands
{
    public class CountryDetailsCommand : ICommand
    {
        private readonly IDatabase db;

        public CountryDetailsCommand(IDatabase db)
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

            int countryId;
            var countryIdParsed = int.TryParse(parameters[0], out countryId);
            if (!countryIdParsed)
            {
                return "Not Valid Country Id. Fill in numeric value!";
            }

            var country = this.db.Countries.SingleOrDefault(c => c.Id == countryId);
            if (country == null)
            {
                return "Country with such id doesn't exist!";
            }

            return country.ToString();
        }
    }
}
