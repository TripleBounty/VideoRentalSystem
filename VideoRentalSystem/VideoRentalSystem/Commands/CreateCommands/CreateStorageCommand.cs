using System;
using System.Collections.Generic;
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
            var storeId = int.Parse(parameters[0]);
            var filmId = int.Parse(parameters[1]);
            var quantity = int.Parse(parameters[2]);
            VideoFormat videoFormat = (VideoFormat)Enum.Parse(typeof(VideoFormat), parameters[3], true);

            var store = this.db.Stores.SingleOrDefault(s => s.Id == storeId);
            var film = this.db.Film.SingleOrDefault(f => f.Id == filmId);

            var storage = this.factory.CreateStorage(store, film, quantity, videoFormat);

            this.db.Storages.Add(storage);
            this.db.Complete();

            return "Storage created";
        }
    }
}
