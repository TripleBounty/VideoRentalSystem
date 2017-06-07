using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class LoadReviewFromJSONCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public LoadReviewFromJSONCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            string fileLoc = parameters[0];
            using (StreamReader r = new StreamReader(fileLoc))
            {
                string json = r.ReadToEnd();
                dynamic data = JObject.Parse(json);
                foreach (var reviews in data.review)
                {
                    foreach (var currentReview in reviews)
                    {
                        int filmId;
                        int customerId;
                        double rating;

                        try
                        {
                            filmId = currentReview.FilmId;
                            customerId = currentReview.CustomerId;
                            rating = currentReview.Rating;
                        }
                        catch (Exception)
                        {
                            throw new ArgumentException(string.Format(
                                "Input parameters are not in the correct format!" +
                                Environment.NewLine +
                                "The correct format is: Rating double;Description string;FilmId int"));
                        }

                        string description = currentReview.Description;

                        var filmObj = this.db.Films.SingleOrDefault(e => e.Id == filmId);
                        if (filmObj == null)
                        {
                            return "Film with such id doesn't exist!";
                        }

                        var customerObj = this.db.Customers.SingleOrDefault(e => e.Id == customerId);
                        if (customerObj == null)
                        {
                            return "Film with such id doesn't exist!";
                        }

                        var review = this.factory.CreateReview(rating, description, filmObj, customerObj);

                        this.db.Reviews.Add(review);
                        this.db.Complete();
                    }
                }
            }

            return "Reviews created";
        }
    }
}