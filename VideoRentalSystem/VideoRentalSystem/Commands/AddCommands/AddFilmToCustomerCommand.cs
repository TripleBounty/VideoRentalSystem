using System;
using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;

namespace VideoRentalSystem.Commands.AddCommands
{
    public class AddFilmToCustomerCommand : ICommand
    {
        private readonly IDatabase db;

        public AddFilmToCustomerCommand(IDatabase db)
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
            
            int filmId;
            var filmIdParsed = int.TryParse(parameters[0], out filmId);
            if (!filmIdParsed)
            {
                return "Not Valid Film Id. Fill in numeric value!";
            }

            var film = this.db.Films.SingleOrDefault(x => x.Id == filmId);
            if (film == null)
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
            
            customer.Films.Add(film);
            this.db.Complete();

            return film.Name + " added to " + customer.FirstName + " " + customer.LastName;
        }
    }
}