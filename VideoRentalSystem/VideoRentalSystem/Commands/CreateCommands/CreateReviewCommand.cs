using System;
using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class CreateReviewCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public CreateReviewCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            int filmId = int.Parse(parameters[0]);
            double rating = double.Parse(parameters[1]);
            string description = parameters[2];

            var filmObj = this.db.Films.SingleOrDefault(e => e.Id == filmId);

            if(filmObj == null)
            {
                throw new ArgumentException(String.Format("You are trying to write a review for a non existing movie !"));
            }

            var review = this.factory.CreateReview(rating, description, filmObj);

            this.db.Reviews.Add(review);
            this.db.Complete();

            return "Review created";
        }
    }
}