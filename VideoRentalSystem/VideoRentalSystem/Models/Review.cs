using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VideoRentalSystem.Models
{
    public class Review
    {
        public Review(double rating, string description, Film film)
        {
            this.Film = film;
            this.Rating = rating;
            this.Description = description;
        }

        public Review()
        {
        }

        public int Id { get; set; }

        public virtual Film Film { get; set; }

        public double Rating { get; set; }

        public string Description { get; set; }

        public override string ToString()
        {
            return string.Format("{0}Rated at:{1} Stars, {2}Decription: {3}{4}", 
                                                          this.Film, this.Rating, System.Environment.NewLine, this.Description, System.Environment.NewLine);
        }
    }
}