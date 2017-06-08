using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data.Postgre.Contracts
{
    public interface ILoanRepository : IRepository<Loan>
    {
    }
}