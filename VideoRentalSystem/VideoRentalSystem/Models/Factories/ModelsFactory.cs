using VideoRentalSystem.Models.Contracts;

namespace VideoRentalSystem.Models.Factories
{
    public class ModelsFactory : IModelsFactory
    {
        public ICountry CreateCountry(string name, string code)
        {
            return new Country(name, code);
        }

        public IEmployees CreateEmployees(string firstName, string lastName, int salary, int managerId)
        {
            return new Employees(firstName, lastName, salary, managerId);
        }
    }
}
