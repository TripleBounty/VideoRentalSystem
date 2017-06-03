using System;
using VideoRentalSystem.Models.Contracts;
using VideoRentalSystem.Models.Enum;

namespace VideoRentalSystem.Models.Factories
{
    public interface IModelsFactory
    {
        Country CreateCountry(string name, string code);

        Town CreateTown(string name, Country country);

        Employee CreateEmployee(string firstName, string lastName, int salary, Employee manager);

        Customer CreateCustomer(string firstName, string lastName, DateTime birthDate);

        Film CreateFilm(string name, string summary, DateTime realiseDate, TimeSpan duration, VideoFormat format, int count, float rating);

        Review CreateReview(double rating, string description);
    }
}
