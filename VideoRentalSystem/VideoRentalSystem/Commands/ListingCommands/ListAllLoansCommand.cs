using System.Collections.Generic;
using VideoRentalSystem.Commands.Contracts;
using VideoRentalSystem.Data.Postgre.Contracts;

namespace VideoRentalSystem.Commands.ListingCommands
{
    public class ListAllLoansCommand : ICommand
    {
        private readonly IDatabasePostgre db;

        public ListAllLoansCommand(IDatabasePostgre db)
        {
            this.db = db;
        }

        public string Execute(IList<string> parameters)
        {
            var loans = this.db.Loans.GetAll();
            if (loans.Count == 0)
            {
                return "No Loan available";
            }

            return string.Join("\n", loans);
        }
    }
}
