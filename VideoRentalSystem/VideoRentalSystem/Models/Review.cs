using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VideoRentalSystem.Models.Contracts;

namespace VideoRentalSystem.Models
{
    public class Review : IReview
    {
        public Review (int filmId, double rating, string description)
        {
            this.FilmId = filmId;
            this.Rating = rating;
            this.Description = description;
        }

        public int Id { get; set; }
        
        public int FilmId { get; set; }
        
        [Range (0.0,10.0)]
        public double Rating { get; set; }


        public string Description { get; set; }

    }
}
