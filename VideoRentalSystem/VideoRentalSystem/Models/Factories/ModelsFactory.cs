using VideoRentalSystem.Models.Contracts;

namespace VideoRentalSystem.Models.Factories
{
    public class ModelsFactory : IModelsFactory
    {
        public Country CreateCountry(string name, string code)
        {
            return new Country(name, code);
        }

        public Employee CreateEmployees(string firstName, string lastName, int salary, int managerId)
        {
            return new Employee(firstName, lastName, salary, managerId);
        }
    }
}
