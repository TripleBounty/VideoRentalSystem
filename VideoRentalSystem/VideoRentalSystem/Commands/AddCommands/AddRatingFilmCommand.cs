using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.AddCommands
{
    public class AddRatingFilmCommand : ICommand
    {
        private IDatabase db;

        public AddRatingFilmCommand(IDatabase data)
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

            var ageGroup = parameters[1];
            var rating = this.db.FilmRating.SingleOrDefault(x => x.AgeRating == ageGroup);
            if (rating == null)
            {
                return "Film rating not found";
            }

            film.Categories.Add(rating);
            this.db.Complete();

            return ageGroup + " added to " + filmName;
        }
    }
}
