using VideoRentalSystem.Models.Enum;

namespace VideoRentalSystem.Models
{
    public class FilmGenre
    {
        public FilmGenre()
        {
        }

        public FilmGenre(string genre)
        {
            this.Genre = genre;
        }

        public int Id { get; set; }

        public string Genre { get; set; }

        public bool IsDeleted { get; set; }
    }
}