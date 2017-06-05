using System;
using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class CreateFilmRating : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public CreateFilmRating(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            var rating = parameters[0];

            var newRating = this.factory.CreateFilmRating(rating);

            db.FilmRating.Add(newRating);
            db.Complete();

            return "new film rating created";
        }
    }
}
