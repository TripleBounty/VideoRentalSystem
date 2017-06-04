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
            return string.Format("{0}, {1}, Genre: {2}, Rated at:{3} Stars, Decription: {4}", 
                                                          this.Id, this.Film.Name, 
                                                          this.Film.Genres, this.Rating, 
                                                          this.Description);
        }
    }
}