using VideoRentalSystem.Data.Postgre.Contracts;
using VideoRentalSystem.Data.Repository;

namespace VideoRentalSystem.Data.Postgre
{
    public class DatabasePostgre : IDatabasePostgre
    {
        private readonly VideoRentalLoanContext context;

        public DatabasePostgre(VideoRentalLoanContext context)
        {
            this.context = context;
            this.Loans = new LoanRepository(context);
            this.Tarifs = new TarifRepository(context);
        }

        public ILoanRepository Loans { get; private set; }

        public ITarifRepository Tarifs { get; private set; }

        public int Complete()
        {
            return this.context.SaveChanges();
        }
    }
}
