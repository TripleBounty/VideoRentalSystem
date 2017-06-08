using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.UpdateCommands
{
    public class UpdateFilmRatingCommand : ICommand
    {
        private IDatabase db;
        private IModelsFactory factory;

        public UpdateFilmRatingCommand(IDatabase db, IModelsFactory factory)
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

            var ratingId = int.Parse(parameters[0]);
            var rating = this.db.FilmRating.SingleOrDefault(x => x.Id == ratingId);

            if (rating == null)
            {
                return "age rating not found";
            }

            var newRatingName = parameters[1];

            rating.AgeRating = newRatingName;
            this.db.Complete();

            return newRatingName + " updated";
        }
    }
}
