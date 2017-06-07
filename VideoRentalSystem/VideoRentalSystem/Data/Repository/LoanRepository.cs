using VideoRentalSystem.Data.Postgre;
using VideoRentalSystem.Data.Postgre.Contracts;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data.Repository
{
    public class LoanRepository : Repository<Loan>, ILoanRepository
    {
        public LoanRepository(VideoRentalLoanContext context)
            : base(context)
        {
        }

        private VideoRentalLoanContext VideoRentalLoanContext
        {
            get { return this.Context as VideoRentalLoanContext; }
        }
    }
}
