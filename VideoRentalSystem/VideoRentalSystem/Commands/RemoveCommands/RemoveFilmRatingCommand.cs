using System;
using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.RemoveCommands
{
    public class RemoveFilmRatingCommand : ICommand
    {
        private IDatabase db;
        private IModelsFactory factory;

        public RemoveFilmRatingCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            var rating = parameters[0];

            var filmRating = this.db.FilmRating.SingleOrDefault(x => x.AgeRating == rating);

            if (filmRating == null)
            {
                return "age rating not found";
            }

            filmRating.IsDeleted = true;
            db.Complete();

            return "Film rating removed";
        }
    }
}
