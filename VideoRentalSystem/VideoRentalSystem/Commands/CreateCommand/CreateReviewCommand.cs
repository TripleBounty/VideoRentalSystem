using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;
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

            var review = this.factory.CreateReview(rating, description);

            this.db.Reviews.Add(review);
            this.db.Complete();

            return "Review created";
        }
    }
}