using System.ComponentModel.DataAnnotations;
using VideoRentalSystem.Models.Contracts;

namespace VideoRentalSystem.Models
{
    public class Review : IReview
    {
        public Review(int filmId, double rating, string description)
        {
            this.FilmId = filmId;
            this.Rating = rating;
            this.Description = description;
        }

        public int Id { get; set; }
        
        public int FilmId { get; set; }
        
        // TODO remove range from review
        [Range(0.0, 10.0)]
        public double Rating { get; set; }

        public string Description { get; set; }
    }
}
