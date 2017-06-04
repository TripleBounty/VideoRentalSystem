using System;
using VideoRentalSystem.Models.Contracts;
using VideoRentalSystem.Models.Enum;

namespace VideoRentalSystem.Models.Factories
{
    public interface IModelsFactory
    {
        Country CreateCountry(string name, string code);

        Employee CreateEmployee(string firstName, string lastName, int salary, Employee manager);

        Film CreateFilm(string name, string summary, DateTime realiseDate, TimeSpan duration, VideoFormat format);

        Review CreateReview(double rating, string description);

        Award CreateAward(string name, DateTime awardDate);
    }
}
