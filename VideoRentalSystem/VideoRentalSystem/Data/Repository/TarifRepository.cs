using VideoRentalSystem.Data.Postgre;
using VideoRentalSystem.Data.Postgre.Contracts;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data.Repository
{
    public class TarifRepository : Repository<Tarif>, ITarifRepository
    {
        public TarifRepository(VideoRentalLoanContext context)
            : base(context)
        {
        }

        private VideoRentalLoanContext VideoRentalLoanContext
        {
            get { return this.Context as VideoRentalLoanContext; }
        }
    }
}
