using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.UpdateCommands
{
    public class UpdateFilmGenreCommand : ICommand
    {
        private IDatabase db;
        private IModelsFactory factory;

        public UpdateFilmGenreCommand(IDatabase db, IModelsFactory factory)
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

            var genreId = int.Parse(parameters[0]);
            var genre = this.db.FilmGenres.SingleOrDefault(x => x.Id == genreId);

            if (genre == null)
            {
                return "genre not found";
            }

            var newGenre = parameters[1];

            genre.Genre = newGenre;
            this.db.Complete();

            return newGenre + " updated";
        }
    }
}
