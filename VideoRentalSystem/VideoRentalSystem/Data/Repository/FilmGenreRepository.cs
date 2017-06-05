using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data.Repository
{
    public class FilmGenreRepository : Repository<FilmGenre>, IFilmGenreRepository
    {
        public FilmGenreRepository(VideoRentalContext context)
            : base(context)
        {
        }

        private VideoRentalContext VideoRentalContext
        {
            get { return this.Context as VideoRentalContext; }
        }
    }
}