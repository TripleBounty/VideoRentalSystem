using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data.Contracts
{
    public interface ICountryRepository
    {
        //TODO: inject interface 
        void CreateCountry(Country country);
    }
}
