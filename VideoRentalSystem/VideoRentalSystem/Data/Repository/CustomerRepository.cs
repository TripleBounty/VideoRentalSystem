using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Models;

namespace VideoRentalSystem.Data.Repository
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(VideoRentalContext context)
            : base(context)
        {
        }

        private VideoRentalContext VideoRentalContext
        {
            get { return this.Context as VideoRentalContext; }
        }
    }
}