using System;
using System.Collections.Generic;
using System.Text;
using VideoRentalSystem.Common.Validations;

namespace VideoRentalSystem.Models
{
    public class Customer
    {
        private string firstName;
        private string lastName;

        public Customer(string firstName, string lastName, DateTime birthDate, ICollection<Film> films, ICollection<FilmGenre> genres)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.Films = new HashSet<Film>(films);
            this.Genres = new HashSet<FilmGenre>(genres);
            this.Reviews = new HashSet<Review>();
        }

        public Customer(string firstName, string lastName, DateTime birthDate)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.Films = new HashSet<Film>();
            this.Genres = new HashSet<FilmGenre>();
            this.Reviews = new HashSet<Review>();
        }

        public Customer()
        {
            this.Films = new HashSet<Film>();
            this.Genres = new HashSet<FilmGenre>();
            this.Reviews = new HashSet<Review>();
        }

        public int Id { get; set; }

        public string FirstName
        {
            get
            {
                return this.firstName;
            }

            set
            {
                EmployeeValidator.ValidateName(value);
                this.firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return this.lastName;
            }

            set
            {
                EmployeeValidator.ValidateLastName(value);
                this.lastName = value;
            }
        }

        public DateTime BirthDate { get; set; }

        public virtual ICollection<Film> Films { get; set; }

        public virtual ICollection<FilmGenre> Genres { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }

        public override string ToString()
        {
            List<string> filmNames = new List<string>();
            foreach (var film in this.Films)
            {
                filmNames.Add(film.Name);
            }

            List<string> genreNames = new List<string>();
            foreach (var genre in this.Genres)
            {
                genreNames.Add(genre.Genre);
            }

            List<string> reviewNames = new List<string>();
            foreach (var review in this.Reviews)
            {
                reviewNames.Add(review.Id.ToString());
                reviewNames.Add(review.Rating.ToString());
                reviewNames.Add(review.Film.Name);
            }

            var sb = new StringBuilder();
            sb.Append("Customer: ");
            sb.AppendLine(this.Id.ToString());
            sb.Append("First Name: ");
            sb.AppendLine(this.firstName);
            sb.Append("Last Name: ");
            sb.AppendLine(this.lastName);
            sb.Append("Born on: ");
            sb.AppendLine(this.BirthDate.ToString());
            sb.Append("Favorite Films: ");
            sb.AppendLine(string.Join(",", filmNames));
            sb.Append("Favorite Genres: ");
            sb.AppendLine(string.Join(",", genreNames));
            sb.Append("My reviews: ");
            sb.AppendLine(string.Join(",", reviewNames));

            return sb.ToString();
        }
    }
}