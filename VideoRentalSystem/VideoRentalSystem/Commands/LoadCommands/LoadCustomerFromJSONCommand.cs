using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class LoadCustomerFromJSONCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IModelsFactory factory;

        public LoadCustomerFromJSONCommand(IDatabase db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            string fileLoc = parameters[0];
            using (StreamReader r = new StreamReader(fileLoc))
            {
                string json = r.ReadToEnd();
                dynamic data = JObject.Parse(json);
                foreach (var employee in data.customer)
                {
                    foreach (var currentEmployee in employee)
                    {
                        string firstName = currentEmployee.FirstName;
                        string lastName = currentEmployee.LastName;
                        int[] filmIds;
                        int[] genreIds;
                        int[] reviewIds;
                        DateTime birthDate;

                        try
                        {
                            birthDate = DateTime.ParseExact(currentEmployee.BirthDate.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                            string filmsList = currentEmployee.Films.ToString();
                            filmIds = filmsList.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                            string genresList = currentEmployee.Genres.ToString();
                            genreIds = genresList.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                            string reviewsList = currentEmployee.Reviews.ToString();
                            reviewIds = reviewsList.Split(',').Select(n => Convert.ToInt32(n)).ToArray();
                        }
                        catch (Exception)
                        {
                            throw new ArgumentException(string.Format(
                                " Input parameters are not in the correct format!" +
                                Environment.NewLine +
                                "The correct format is: FirstName string;LastName string;Birthdate dd/mm/yyyy; FilmIds 1,2,3; GenreIds 1,2,3; My Reviews 1,2,3"));
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
                            var reviewObj = this.db.FilmGenres.SingleOrDefault(e => e.Id == review);
                            if (reviewObj == null)
                            {
                                return "Genre with such id doesn't exist!";
                            }

                            genres.Add(reviewObj);
                        }

                        var customer = this.factory.CreateCustomer(firstName, lastName, birthDate, films, genres, reviews);

                        this.db.Customers.Add(customer);
                        this.db.Complete();
                    }
                }
            }

            return "Customers created";
        }
    }
}