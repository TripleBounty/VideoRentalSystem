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

            this.db.FilmGenres.Add(newGenre);
            this.db.Complete();

            return "new genre created";
        }
    }
}
