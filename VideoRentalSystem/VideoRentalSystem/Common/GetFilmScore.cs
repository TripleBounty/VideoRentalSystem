using System;
using System.Linq;
using VideoRentalSystem.Common.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Common
{
    public class GetFilmScore : IGetFilmScore
    {
        private readonly IDatabase db;

        public GetFilmScore(IDatabase db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("Null database value is passed for GetFilmScore parameter");
            }

            this.db = db;
        }

        public double GetAvgFilmScore(string filmName)
        {
            double score = 0;

            if (filmName == string.Empty || filmName == null)
            {
                throw new ArgumentNullException("film not provided");
            }

            var film = this.db.Films.SingleOrDefault(x => x.Name == filmName);

            if (film == null)
            {
                throw new ArgumentException("Film was not found");
            }

            var reviews = (from review in this.db.Reviews.GetAll()
                           where review.Film.Id == film.Id
                           select review).ToList();

            foreach (var r in reviews)
            {
                score += r.Rating;
            }

            score = score / reviews.Count;

            return score;
        }
    }
}
