using VideoRentalSystem.Models.Contracts;

namespace VideoRentalSystem.Models.Factories
{
    public interface IModelsFactory
    {
        ICountry CreateCountry(string name, string code);
    }
}
