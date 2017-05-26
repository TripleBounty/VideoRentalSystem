using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;
using VideoRentalSystem.Models.Contracts;

namespace VideoRentalSystem.Data.Repository
{
    //TODO: may or maynot be coupled
    public class CountryRepository : Repository<Country>, ICountryRepository
    {

        public CountryRepository(VideoRentalContext context)
            : base(context)
        {
        }

        private VideoRentalContext VideoRentalContext
        {
            get { return Context as VideoRentalContext; }
        }
    }
}

