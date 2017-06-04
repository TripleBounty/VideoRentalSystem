using System.Collections.Generic;
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
            var storeName = parameters[0];
            var addressId = int.Parse(parameters[1]);

            var address = this.db.Addesses.SingleOrDefault(c => c.Id == addressId);

            var store = this.factory.CreateStore(storeName, address);

            this.db.Stores.Add(store);
            this.db.Complete();

            return "Store created";
        }
    }
}
