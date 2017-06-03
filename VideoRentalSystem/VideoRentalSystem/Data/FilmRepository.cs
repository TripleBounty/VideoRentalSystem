using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Data.Repository;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data
{
    public class FilmRepository : Repository<Film>, IFilmRepository
    {
        public FilmRepository(VideoRentalContext context) 
            : base(context)
        {
        }

        private VideoRentalContext VideoRentalContext
        {
            get { return this.Context as VideoRentalContext; }
        }
    }
}
