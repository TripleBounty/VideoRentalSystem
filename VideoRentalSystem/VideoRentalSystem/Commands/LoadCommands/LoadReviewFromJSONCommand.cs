using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
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
                        int filmId = currentReview.FilmId;
                        double rating = currentReview.Rating;
                        string description = currentReview.Description;

                        var filmObj = this.db.Film.SingleOrDefault(e => e.Id == filmId);

                        var review = this.factory.CreateReview(rating, description, filmObj);

                        this.db.Reviews.Add(review);
                        this.db.Complete();
                    }
                }
            }

            return "Reviews created";
        }
    }
}