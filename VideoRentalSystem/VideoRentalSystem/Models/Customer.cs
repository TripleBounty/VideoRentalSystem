using System;
using VideoRentalSystem.Common.Validations;

namespace VideoRentalSystem.Models
{
    public class Customer
    {
        private string firstName;
        private string lastName;
        private DateTime birthDate;
       // private Actor favouriteActors;

        public Customer(string firstName, string lastName, DateTime birthDate)//,Actor favouriteActors)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            // this.FavouriteActors = facouriteActors;
        }

        private Customer()
        {
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

       // public ICollection<Actor> Actors { get; set; }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, born on: {3}", this.Id, this.firstName, this.lastName, this.BirthDate.ToString());
        }
    }
}