using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class CreateStoreCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public CreateStoreCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
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

            var storeName = parameters[0];
            int addressId;
            var addressIdParsed = int.TryParse(parameters[1], out addressId);
            if (!addressIdParsed)
            {
                return "Not Valid Address Id. Fill in numeric value!";
            }

            var address = this.db.Addesses.SingleOrDefault(c => c.Id == addressId);

            if (address == null)
            {
                return "Address with such id doesn't exist!";
            }

            var store = this.factory.CreateStore(storeName, address);

            this.db.Stores.Add(store);
            this.db.Complete();

            return "Store created";
        }
    }
}
