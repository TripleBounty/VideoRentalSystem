using VideoRentalSystem.Data.Contracts;
using VideoRentalSystem.Data.Repository;

namespace VideoRentalSystem.Data
{
    public class Database : IDatabase
    {
        private readonly VideoRentalContext context;

        public Database(VideoRentalContext context)
        {
            this.context = context;
            this.Users = new UserRepository(context);
            this.Countries = new CountryRepository(context);
            this.Employees = new EmployeesRepository(context);
        }

        public IUserRepository Users { get; private set; }
        public ICountryRepository Countries { get; private set; }
        public IEmployeesRepository Employees { get; private set; }

        public int Complete()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
