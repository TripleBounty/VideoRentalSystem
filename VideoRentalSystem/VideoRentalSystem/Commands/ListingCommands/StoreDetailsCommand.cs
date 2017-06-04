using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.ListingCommands
{
    public class StoreDetailsCommand : ICommand
    {
        private readonly IDatabase db;

        public StoreDetailsCommand(IDatabase db)
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

            int storeId;
            var storeIdParsed = int.TryParse(parameters[0], out storeId);
            if (!storeIdParsed)
            {
                return "Not Valid Store Id. Fill in numeric value!";
            }

            var store = this.db.Stores.SingleOrDefault(c => c.Id == storeId);
            if (store == null)
            {
                return "Store with such id doesn't exist!";
            }

            return store.ToString();
        }
    }
}
