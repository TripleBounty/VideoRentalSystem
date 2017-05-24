using VideoRentalSystem.Models.Contracts;

namespace VideoRentalSystem.Models.Factories
{
    public class ModelsFactory : IModelsFactory
    {
        public ICountry CreateCountry(string name, string code)
        {
            return new Country(name, code);
        }
    }
}
