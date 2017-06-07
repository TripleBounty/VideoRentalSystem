using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Data.Postgre.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class CreateLoanCommand : ICommand
    {
        private readonly IDatabase db;
        private readonly IDatabasePostgre dbP;
        private readonly IModelsFactory factory;

        public CreateLoanCommand(IDatabase db, IDatabasePostgre dbP, IModelsFactory factory)
        {
            this.db = db;
            this.dbP = dbP;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 3)
            {
                return "Not valid number of parameters";
            }

            if (parameters.Any(x => x == string.Empty))
            {
                return "Some of the passed parameters are empty!";
            }

            int storeId;
            var storeIdParsed = int.TryParse(parameters[1], out storeId);
            if (!storeIdParsed)
            {
                return "Not Valid Store parameter. Fill in numeric value!";
            }

            var store = this.db.Stores.SingleOrDefault(s => s.Id == storeId);

            if (store == null)
            {
                return "Store with such Id doesn't exist";
            }

            int filmId;
            var filmIdParsed = int.TryParse(parameters[1], out filmId);
            if (!filmIdParsed)
            {
                return "Not Valid Film id parameter. Fill in numeric value!";
            }

            var film = this.db.Films.SingleOrDefault(f => f.Id == filmId);

            if (film == null)
            {
                return "Film with such Id doesn't exist";
            }

            int customerId;
            var customerIdParsed = int.TryParse(parameters[1], out customerId);
            if (!customerIdParsed)
            {
                return "Not Valid Customer parameter. Fill in numeric value!";
            }

            var customer = this.db.Customers.SingleOrDefault(c => c.Id == customerId);

            if (customer == null)
            {
                return "Customer with such Id doesn't exist";
            }

            var loan = this.factory.CreateLoan(storeId, filmId, customerId);

            this.dbP.Loans.Add(loan);
            this.dbP.Complete();

            return "Loan created";
        }
    }
}
