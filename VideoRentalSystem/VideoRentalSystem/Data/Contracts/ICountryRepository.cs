using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data.Contracts
{
    public interface ICountryRepository
    {
        void CreateCountry(Country country);
    }
}
