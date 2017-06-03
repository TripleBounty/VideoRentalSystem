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

        public Employee CreateEmployee(string firstName, string lastName, int salary, Employee manager)
        {
            return new Employee(firstName, lastName, salary, manager);
        }

        public Review CreateReview(double rating, string description)
        {
            return new Review( rating, description);
        }

        public Film CreateFilm(string name, string summary, DateTime realiseDate, TimeSpan duration, VideoFormat format, int count)
        {
            return new Film(name, summary, realiseDate, duration, format, count);
        }
    }
}
