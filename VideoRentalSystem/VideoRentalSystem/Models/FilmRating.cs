using VideoRentalSystem.Models.Enum;

namespace VideoRentalSystem.Models
{
    public class FilmRating
    {
        public FilmRating()
        {
        }

        public FilmRating(string rating)
        {
            this.AgeRating = rating;
        }

        public int Id { get; set; }

        public string AgeRating { get; set; }

        public bool IsDeleted { get; set; }
    }
}