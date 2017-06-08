using System;
using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.RemoveCommands
{
    public class RemoveFilmCommand : ICommand
    {
        private IDatabase db;
        private IModelsFactory factory;

        public RemoveFilmCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            var filmName = parameters[0];

            var film = this.db.Films.SingleOrDefault(x => x.Name == filmName);

            if (film == null)
            {
                return "film not found";
            }

            film.IsDeleted = true;
            db.Complete();

            return "Film removed";
        }
    }
}
