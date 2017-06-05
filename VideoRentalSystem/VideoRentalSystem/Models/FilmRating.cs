using VideoRentalSystem.Models.Enum;

namespace VideoRentalSystem.Models
{
    public class FilmRating
    {
        public FilmRating()
        {
        }

        public FilmRating(MPAA_Rating rating)
        {
            this.AgeRating = rating;
        }

        public int Id { get; set; }

        public MPAA_Rating AgeRating { get; set; }

        public override string ToString()
        {
            return AgeRating.ToString();
        }
    }
}