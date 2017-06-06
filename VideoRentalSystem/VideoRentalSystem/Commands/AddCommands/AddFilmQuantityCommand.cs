using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.AddCommands
{
    public class AddFilmQuantityCommand : ICommand
    {
        private readonly IDatabase db;

        public AddFilmQuantityCommand(IDatabase db)
        {
            this.db = db;
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

            int storageId;
            var storageIdParsed = int.TryParse(parameters[0], out storageId);
            if (!storageIdParsed)
            {
                return "Not Valid Storage Id. Fill in numeric value!";
            }

            var storage = this.db.Storages.SingleOrDefault(s => s.Id == storageId);
            if (storage == null)
            {
                return "Storage with such id doesn't exist!";
            }

            int quantity;
            var quantityParsed = int.TryParse(parameters[1], out quantity);
            if (!quantityParsed || quantity <= 0)
            {
                return "Not Valid Quantity!";
            }

            storage.Quantity += quantity;

            this.db.Complete();

            return "Quantity updated";
        }
    }
}
