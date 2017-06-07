using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class CreateCustomerCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public CreateCustomerCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 6)
            {
                return "Not valid number of parameters";
            }

            if (parameters.Any(x => x == string.Empty))
            {
                return "Some of the passed parameters are empty!";
            }

            var firstName = parameters[0];
            var lastName = parameters[1];
            int[] filmIds;
            int[] genreIds;
            int[] reviewIds;
            DateTime birthDate;

            try
            {
                birthDate = DateTime.ParseExact(parameters[2], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                filmIds = parameters[3].Split(',').Select(int.Parse).ToArray();
                genreIds = parameters[4].Split(',').Select(int.Parse).ToArray();
                reviewIds = parameters[5].Split(',').Select(int.Parse).ToArray();
            }
            catch (Exception)
            {
                throw new ArgumentException(string.Format(
                    "Input parameters are not in the correct format!" +
                    Environment.NewLine +
                    "The correct format is: FirstName string;LastName string;Birthdate dd/mm/yyyy; FilmIds 1,2,3; GenreIds 1,2,3"));
            }

            List<Film> films = new List<Film>();
            List<FilmGenre> genres = new List<FilmGenre>();
            List<Review> reviews = new List<Review>();

            foreach (var film in filmIds)
            {
                var filmObj = this.db.Films.SingleOrDefault(e => e.Id == film);
                if (filmObj == null)
                {
                    return "Film with such id doesn't exist!";
                }

                films.Add(filmObj);
            }

            foreach (var genre in genreIds)
            {
                var genreObj = this.db.FilmGenres.SingleOrDefault(e => e.Id == genre);
                if (genreObj == null)
                {
                    return "Genre with such id doesn't exist!";
                }

                genres.Add(genreObj);
            }

            foreach (var review in reviewIds)
            {
                var reviewObj = this.db.Reviews.SingleOrDefault(e => e.Id == review);
                
                if (reviewObj == null)
                {
                    return "Review with such id doesn't exist!";
                }

                reviews.Add(reviewObj);
            }

            var customer = this.factory.CreateCustomer(firstName, lastName, birthDate, films, genres, reviews);
            
            this.db.Customers.Add(customer);
            this.db.Complete();

            return "Customer created";
        }
    }
}