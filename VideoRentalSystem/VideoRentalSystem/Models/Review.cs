using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VideoRentalSystem.Models
{
    public class Review
    {
        public Review(double rating, string description)
        {
            ////this.Film = film;
            this.Rating = rating;
            this.Description = description;
        }

        public Review()
        {
        }

        public int Id { get; set; }

        public virtual ICollection<Film> Film { get; set; }

        public double Rating { get; set; }

        public string Description { get; set; }
    }
}
