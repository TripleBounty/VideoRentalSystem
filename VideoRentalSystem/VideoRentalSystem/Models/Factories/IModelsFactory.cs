using VideoRentalSystem.Models.Contracts;

namespace VideoRentalSystem.Models.Factories
{
    public interface IModelsFactory
    {
        Country CreateCountry(string name, string code);

        Employee CreateEmployees(string firstName, string lastName, int salary, int managerId);

        Review CreateReview(int filmId, double rating, string description);
    }
}
