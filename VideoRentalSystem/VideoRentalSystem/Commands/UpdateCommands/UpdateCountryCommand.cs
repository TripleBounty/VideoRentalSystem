using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.UpdateCommands
{
    public class UpdateCountryCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public UpdateCountryCommand(IDatabase db, IModelsFactory factory)
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

            var countryName = parameters[1];
            var countryCode = parameters[2];

            country.Name = countryName;
            country.Code = countryCode;

            this.db.Complete();

            return "Country updated";
        }
    }
}