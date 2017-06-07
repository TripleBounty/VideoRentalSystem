using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Postgre.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.CreateCommands
{
    public class CreateLoanCommand : ICommand
    {
        private readonly IDatabasePostgre db;
        private readonly IModelsFactory factory;

        public CreateLoanCommand(IDatabasePostgre db, IModelsFactory factory)
        {
            this.db = db;
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

            int filmId;
            var filmIdParsed = int.TryParse(parameters[1], out filmId);
            if (!filmIdParsed)
            {
                return "Not Valid Store parameter. Fill in numeric value!";
            }

            int customerId;
            var customerIdParsed = int.TryParse(parameters[1], out customerId);
            if (!customerIdParsed)
            {
                return "Not Valid Store parameter. Fill in numeric value!";
            }

            var loan = this.factory.CreateLoan(storeId, filmId, customerId);

            this.db.Loans.Add(loan);
            this.db.Complete();

            return "Loan created";
        }
    }
}
