using System;
using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Enum;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class CreateStorageCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public CreateStorageCommand(IDatabase db, IModelsFactory factory)
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

            int storeId;
            var storeIdParsed = int.TryParse(parameters[0], out storeId);
            if (!storeIdParsed)
            {
                return "Not Valid Store Id. Fill in numeric value!";
            }

            var store = this.db.Stores.SingleOrDefault(s => s.Id == storeId);
            if (store == null)
            {
                return "Store with such id doesn't exist!";
            }

            int filmId;
            var filmIdParsed = int.TryParse(parameters[1], out filmId);
            if (!filmIdParsed)
            {
                return "Not Valid Film Id. Fill in numeric value!";
            }

            var film = this.db.Films.SingleOrDefault(f => f.Id == filmId);
            if (film == null)
            {
                return "Film with such id doesn't exist!";
            }

            int quantity;
            var quantityParsed = int.TryParse(parameters[2], out quantity);
            if (!quantityParsed || quantity <= 0)
            {
                return "Not Valid Quantity. Fill in numeric value!";
            }

            VideoFormat videoFormat;
            var videoFormatParsed = Enum.TryParse<VideoFormat>(parameters[3], true, out videoFormat);
            if (!videoFormatParsed)
            {
                return "Not Valid Video Format!";
            }

            var storage = this.factory.CreateStorage(store, film, quantity, videoFormat);

            this.db.Storages.Add(storage);
            this.db.Complete();

            return "Storage created";
        }
    }
}
