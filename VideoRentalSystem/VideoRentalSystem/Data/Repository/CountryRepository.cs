using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;
using VideoRentalSystem.Models.Contracts;

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

