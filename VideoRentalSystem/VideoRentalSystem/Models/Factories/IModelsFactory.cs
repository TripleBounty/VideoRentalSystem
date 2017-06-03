using System.Collections.Generic;
using VideoRentalSystem.Models.Contracts;

namespace VideoRentalSystem.Models.Factories
{
    public interface IModelsFactory
    {
        Country CreateCountry(string name, string code);

        Employee CreateEmployee(string firstName, string lastName, int salary, Employee manager);

        Review CreateReview(double rating, string description);
    }
}
