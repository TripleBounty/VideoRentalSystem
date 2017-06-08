using System.Linq;
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

        public bool CheckTarif(int tarifId)
        {
            var tarifs =
                       from loan in this.VideoRentalLoanContext.LoansTable
                       join tarif in this.VideoRentalLoanContext.TarifsTable
                       on loan.Tarif equals tarif
                       where tarif.Id == tarifId
                       select tarif;
            int count = tarifs.Count<Tarif>();

            if (count > 0)
            {
                return true;
            }

            return false;
        }
    }
}
