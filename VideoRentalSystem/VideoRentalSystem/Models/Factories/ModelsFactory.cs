using System;
using System.Collections;
using System.Collections.Generic;
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

        public Customer CreateCustomer(string firstName, string lastName, DateTime birthDate, List<Film> films, List<FilmGenre> genres)
        {
            return new Customer(firstName, lastName, birthDate, films, genres);
        }

        public Review CreateReview(double rating, string description, Film film, Customer customer)
        {
            return new Review(rating, description, film, customer);
        }

        public Film CreateFilm(string name, string summary, DateTime realiseDate, TimeSpan duration)
        {
            return new Film(name, summary, realiseDate, duration);
        }

        public FilmRating CreateFilmRating(string rating)
        {
            return new FilmRating(rating);
        }

        public FilmGenre CreateFilmGenre(string genre)
        {
            return new FilmGenre(genre);
        }

        public Award CreateAward(string name, string year, long orgId)
        {
            return new Award(name, year, orgId);
        }

        public Organisation CreateOrganisation(string name)
        {
            return new Organisation(name);
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

        public Loan CreateLoan(int storeId, int filmId, int customerId)
        {
            return new Loan(storeId, filmId, customerId);
        }

        public Tarif CreateTarif(string name, int maxNumberOfDays, decimal price)
        {
            return new Tarif(name, maxNumberOfDays, price);
        }
    }
}