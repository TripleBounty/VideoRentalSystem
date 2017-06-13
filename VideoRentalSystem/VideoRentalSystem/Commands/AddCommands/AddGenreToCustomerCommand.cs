using System;
using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.AddCommands
{
    public class AddGenreToCustomerCommand : ICommand
    {
        private readonly IDatabase db;

        public AddGenreToCustomerCommand(IDatabase db)
        {
            if (db == null)
            {
                throw new ArgumentNullException("Invalid database in AddFilmQuantityCommand constructor");
            }

            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 2)
            {
                return "Not valid number of parameters";
            }

            if (parameters.Any(x => x == string.Empty))
            {
                return "Some of the passed parameters are empty!";
            }

            int genreId;
            var genreIdParsed = int.TryParse(parameters[0], out genreId);
            if (!genreIdParsed)
            {
                return "Not Valid Genre Id. Fill in numeric value!";
            }

            var genre = this.db.FilmGenres.SingleOrDefault(x => x.Id == genreId);
            if (genre == null)
            {
                return "Film not found";
            }

            int customerId;
            var customerIdParsed = int.TryParse(parameters[1], out customerId);
            if (!customerIdParsed)
            {
                return "Not Valid Customer Id. Fill in numeric value!";
            }

            var customer = this.db.Customers.SingleOrDefault(x => x.Id == customerId);
            if (customer == null)
            {
                return "Customer not found";
            }

            customer.Genres.Add(genre);
            this.db.Complete();

            return genre.Genre + " added to " + customer.FirstName + " " + customer.LastName;
        }
    }
}