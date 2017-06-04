using System;
using VideoRentalSystem.Models.Enum;

namespace VideoRentalSystem.Models.Factories
{
    public class ModelsFactory : IModelsFactory
    {
        public Country CreateCountry(string name, string code)
        {
            return new Country(name, code);
        }

        public Town CreateTown(string name, Country country)
        {
            return new Town(name, country);
        }

        public Address CreateAddress(string street, string postalCode, Town town)
        {
            return new Address(street, postalCode, town);
        }

        public Employee CreateEmployee(string firstName, string lastName, int salary, Employee manager)
        {
            return new Employee(firstName, lastName, salary, manager);
        }

        public Customer CreateCustomer(string firstName, string lastName, DateTime birthDate)
        {
            return new Customer(firstName, lastName, birthDate);
        }

        public Review CreateReview(double rating, string description, Film film)
        {
            return new Review(rating, description, film);
        }

        public Film CreateFilm(string name, string summary, DateTime realiseDate, TimeSpan duration)
        {
            return new Film(name, summary, realiseDate, duration);
        }

        public Award CreateAward(string name, DateTime awardDate)
        {
            return new Award(name, awardDate);
        }

        public Store CreateStore(string name, Address address)
        {
            return new Store(name, address);
        }

        public Storage CreateStorage(Store store, Film film, int quantity, VideoFormat videoFormat)
        {
            return new Storage(store, film, quantity, videoFormat);
        }

        public FilmStaff CreateFilmStaff(string firstName, string lastName, DateTime birthDate, Country originePlace, StaffType type)
        {
            return new FilmStaff(firstName, lastName, birthDate, originePlace, type);
        }
    }
}
