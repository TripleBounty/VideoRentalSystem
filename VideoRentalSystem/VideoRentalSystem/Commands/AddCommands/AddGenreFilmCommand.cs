using System;
using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.AddCommands
{
    public class AddGenreFilmCommand : ICommand
    {
        private IDatabase db;

        public AddGenreFilmCommand(IDatabase data)
        {
            this.db = data;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 2)
            {
                return "Not valid number of parameters";
            }

            var filmName = parameters[0];
            var film = this.db.Films.SingleOrDefault(x => x.Name == filmName);
            if (film == null)
            {
                return "Film not found";
            }

            var genreName = parameters[1];
            var genre = this.db.FilmGenres.SingleOrDefault(x => x.Genre == genreName);
            if (genre == null)
            {
                return "Genre not found";
            }

            film.Genres.Add(genre);
            this.db.Complete();

            return genreName + " added to " + filmName;
        }
    }
}
