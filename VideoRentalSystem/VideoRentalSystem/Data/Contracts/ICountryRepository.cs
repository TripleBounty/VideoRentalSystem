using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data.Contracts
{
    public interface ICountryRepository
    {
        void CreateCountry(Country country);
    }
}
