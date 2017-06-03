using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data.Repository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        public CountryRepository(VideoRentalContext context)
            : base(context)
        {
        }

        private VideoRentalContext VideoRentalContext
        {
            get { return this.Context as VideoRentalContext; }
        }
    }
}