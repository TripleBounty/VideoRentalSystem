using VideoRentalSystem.Models.Enum;

namespace VideoRentalSystem.Models
{
    public class FilmGenre
    {
        public FilmGenre()
        {}

        public FilmGenre(Genre genre)
        {
            this.Genre = genre;
        }

        public int Id { get; set; }

        public Genre Genre { get; set; }

        public override string ToString()
        {
            return Genre.ToString();
        }
    }
}