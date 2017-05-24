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

        public void CreateCountry(Country country)
        {
            VideoRentalContext.CountriesTable.Add(country);
        }

        private VideoRentalContext VideoRentalContext
        {
            get { return Context as VideoRentalContext; }
        }
    }
}

