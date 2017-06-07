using System;
using System.Collections.Generic;
using VideoRentalSystem.Models.Enum;

namespace VideoRentalSystem.Models.Factories
{
    public interface IModelsFactory
    {
        Country CreateCountry(string name, string code);

        Town CreateTown(string name, Country country);

        Address CreateAddress(string street, string postalCode, Town town);

        Employee CreateEmployee(string firstName, string lastName, int salary, Employee manager);

        Film CreateFilm(string name, string summary, DateTime realiseDate, TimeSpan duration);

        FilmRating CreateFilmRating(string rating);

        FilmGenre CreateFilmGenre(string genre);

        Customer CreateCustomer(string firstName, string lastName, DateTime birthDate, List<Film> films, List<FilmGenre> genres, List<Review> reviews);

        Review CreateReview(double rating, string description, Film film);

        Store CreateStore(string name, Address address);

        Storage CreateStorage(Store store, Film film, int quantity, VideoFormat videoFormat);

        FilmStaff CreateFilmStaff(string firstName, string lastName, DateTime birthDate, Country originePlace, StaffType type);

        Award CreateAward(string name, DateTime awardDate);
    }
}
