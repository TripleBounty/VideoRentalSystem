using System;
using VideoRentalSystem.Models.Contracts;
using VideoRentalSystem.Models.Enum;

namespace VideoRentalSystem.Models.Factories
{
    public class ModelsFactory : IModelsFactory
    {
        public Country CreateCountry(string name, string code)
        {
            return new Country(name, code);
        }

        public Employee CreateEmployee(string firstName, string lastName, int salary, int managerId)
        {
            return new Employee(firstName, lastName, salary, managerId);
        }

        public Review CreateReview(int filmId, double rating, string description)
        {
            return new Review(filmId, rating, description);
        }

        public Film CreateFilm(string name, string summary, DateTime realiseDate, TimeSpan duration, VideoFormat format, int count, float rating)
        {
            return new Film(name, summary, realiseDate, duration, format, count, rating);
        }
    }
}
