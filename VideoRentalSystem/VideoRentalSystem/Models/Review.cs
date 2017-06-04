using System.ComponentModel.DataAnnotations;
using VideoRentalSystem.Models.Contracts;

namespace VideoRentalSystem.Models
{
    public class Review : IReview
    {
        public Review(double rating, string description)
        {
            ////this.Film = film;
            this.Rating = rating;
            this.Description = description;
        }

        private Review()
        {
        }

        public int Id { get; set; }

        ////  public virtual ICollection<Film> Film { get; set; }

        public double Rating { get; set; }

        public string Description { get; set; }
    }
}
