using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.RemoveCommands
{
    public class RemoveFilmGenreCommand : ICommand
    {
        private IDatabase db;
        private IModelsFactory factory;

        public RemoveFilmGenreCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            var genre = parameters[0];

            var filmGenre = this.db.FilmGenres.SingleOrDefault(x => x.Genre == genre);

            if (filmGenre == null)
            {
                return "genre not found";
            }

            filmGenre.IsDeleted = true;
            this.db.Complete();

            return "Film genre removed";
        }
    }
}
