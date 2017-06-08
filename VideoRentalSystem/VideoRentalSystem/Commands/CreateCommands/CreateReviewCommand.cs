using System;
using System.Collections.Generic;
using System.Linq;
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
            if (parameters.Count != 4)
            {
                return "Not valid number of parameters";
            }

            if (parameters.Any(x => x == string.Empty))
            {
                return "Some of the passed parameters are empty!";
            }

            int filmId = int.Parse(parameters[0]);
            int customerId = int.Parse(parameters[1]);
            double rating = double.Parse(parameters[2]);
            string description = parameters[3];

            var filmObj = this.db.Films.SingleOrDefault(e => e.Id == filmId);

            if (filmObj == null)
            {
                throw new ArgumentException(string.Format("You are trying to write a review for a non existing movie !"));
            }

            var customerObj = this.db.Customers.SingleOrDefault(e => e.Id == customerId);

            if (customerObj == null)
            {
                throw new ArgumentException(string.Format("A non existant customer is trying to write a review !"));
            }

            var review = this.factory.CreateReview(rating, description, filmObj, customerObj);

            this.db.Reviews.Add(review);
            this.db.Complete();

            return "Review created";
        }
    }
}