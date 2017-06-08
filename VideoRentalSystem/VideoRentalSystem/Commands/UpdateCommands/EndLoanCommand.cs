using System.Collections.Generic;
using System.Linq;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Common;
using VideoRentalSystem.Data.Postgre.Contracts;
using VideoRentalSystem.Models.Factories;

namespace VideoRentalSystem.Commands.UpdateCommands
{
    public class EndLoanCommand : ICommand
    {
        private readonly IDatabasePostgre db;
        private readonly IModelsFactory factory;

        public EndLoanCommand(IDatabasePostgre db, IModelsFactory factory)
        {
            this.db = db;
            this.factory = factory;
        }

        public string Execute(IList<string> parameters)
        {
            if (parameters.Count != 1)
            {
                return "Not valid number of parameters";
            }

            if (parameters.Any(x => x == string.Empty))
            {
                return "Some of the passed parameters are empty!";
            }

            int loanId;
            var loanIdParsed = int.TryParse(parameters[0], out loanId);
            if (!loanIdParsed)
            {
                return "Not Valid Loan parameter. Fill in numeric value!";
            }

            var loan = this.db.Loans.SingleOrDefault(l => l.Id == loanId);

            if (loan == null)
            {
                return "Loan with such id doesn't exist";
            }

            loan.EndDate = TimeProvider.Current.UtcNow;
            int days = (loan.EndDate - loan.StartDate).Days;

            var tarif = this.db.Tarifs.Find(t => t.MaxNumberOfDays > days).OrderBy(t => t.MaxNumberOfDays).First();

            loan.Tarif = tarif;

            this.db.Complete();

            return "Loan returned";
        }
    }
}
