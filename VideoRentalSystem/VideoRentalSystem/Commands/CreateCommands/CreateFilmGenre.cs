using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class CreateFilmGenre : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public CreateFilmGenre(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            var genre = parameters[0];
            var newGenre = this.factory.CreateFilmGenre(genre);

            db.FilmGenres.Add(newGenre);
            db.Complete();

            return "new genre created";
        }
    }
}
